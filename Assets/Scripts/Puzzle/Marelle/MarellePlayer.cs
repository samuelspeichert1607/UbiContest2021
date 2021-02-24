using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarellePlayer : MonoBehaviour
{

    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;
    private bool enterBool = true;
    private GameObject colliderObject = null;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(bottomObject.transform.position, Vector3.down, out hit, raycastRange))
        {

            colliderObject = hit.collider.gameObject;
            if (enterBool && colliderObject.tag == "MarelleTile")
            {
                enterBool = false;
                colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);


            }

        }
        else
        {
            if (colliderObject != null && colliderObject.tag == "MarelleTile")
            {
                colliderObject.transform.parent.GetComponent<ParentTile>().CollisionExited(colliderObject);
            }
            enterBool = true;
            colliderObject = null;
        }
    }
}