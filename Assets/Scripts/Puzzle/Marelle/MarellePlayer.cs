using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MarellePlayer : MonoBehaviour
{
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;
    private bool enterBool = true;

    private bool wasMute = false;

    private GameObject colliderObject = null;
    private GameObject speaker;
    private AudioSource sound;

    private void Start()
    {
        speaker = transform.GetChild(1).gameObject;
    }
    void Update()
    {

        
        RaycastHit hit;
        if (Physics.Raycast(bottomObject.transform.position, Vector3.down, out hit, raycastRange))
        {


            if (enterBool)
            {
                colliderObject = hit.collider.gameObject;
                if (wasMute && colliderObject.tag != "MutePlateforme")
                {
                    speaker.SetActive(true);
                    wasMute = false;
                    sound.Play();

                }
                enterBool = false;
                switch (colliderObject.tag)
                {
                    case "MarelleTile":
                        colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);
                        break;
                    case "MutePlateforme":
                        if (!wasMute)
                        {
                            sound = colliderObject.GetComponent<AudioSource>();
                            sound.Play();
                            wasMute = true;
                            speaker.SetActive(false);
                        }
                        break;
                }


            }


        }
        else
        {
            enterBool = true;

        }

        //RaycastHit borderHit;
        //if(Physics.Raycast(bottomObject.transform.position,Vector3.down,out borderHit, noJumpRayRange))
        //{
        //    borderBool = true;//on veut entrer une fois dans le else if
        //}
        //else if(borderBool)
        //{
        //    borderBool = false;
        //    
        //}
    }


}