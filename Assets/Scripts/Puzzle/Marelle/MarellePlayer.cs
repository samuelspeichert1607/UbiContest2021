using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarellePlayer : MonoBehaviour
{
    [SerializeField] private float muteRaycast = 3;
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;
    private bool enterBool = true;
    private GameObject colliderObject = null;
    private GameObject speaker;

    private float rayRange;
    private void Start()
    {
        speaker = transform.GetChild(1).gameObject;
        rayRange = raycastRange;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(bottomObject.transform.position, Vector3.down, out hit, rayRange))
        {

            colliderObject = hit.collider.gameObject;
            if (enterBool)
            {
                enterBool = false;
                switch (colliderObject.tag)
                {
                    case "MarelleTile":
                        colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);
                        break;
                    case "MutePlateforme":
                        speaker.SetActive(false);
                        rayRange = muteRaycast; //je change la range pour qu<on reste muter quand on saute
                        break;
                }
            }
            //if (enterBool && colliderObject.tag == "MarelleTile")
            //{
            //    enterBool = false;
            //    colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);


            //}

        }
        else
        {
            if (colliderObject != null && colliderObject.tag == "MutePlateforme")
            {
                speaker.SetActive(true);
                rayRange = raycastRange;
            }
            enterBool = true;
            colliderObject = null;
        }
    }
}