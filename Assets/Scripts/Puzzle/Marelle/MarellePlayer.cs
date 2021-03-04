using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MarellePlayer : MonoBehaviour
{
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;
    private bool hasExited = true;

    private bool wasMute = false;

    private GameObject colliderObject = null;
    private AudioSource speaker;
    private AudioSource sound;

    private void Start()
    {
        speaker = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {

        
        RaycastHit hit;
        if (Physics.Raycast(bottomObject.transform.position, Vector3.down, out hit, raycastRange))
        {
            colliderObject = hit.collider.gameObject;
            if (wasMute && !colliderObject.CompareTag("MutePlateforme"))
            {
                speaker.mute = false;
                //speaker.SetActive(true);
                wasMute = false;
                sound.Play();
                

            }

            if (hasExited)
            {

                hasExited = false;
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
                            //speaker.SetActive(false);
                            speaker.mute = true;
                        }
                        break;
                }


            }


        }
        else
        {
            hasExited = true;

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