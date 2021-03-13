using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CollisionController : MonoBehaviour
{
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;

    private bool wasMute = false;

    private GameObject colliderObject = null;
    private GameObject previousColliderObject = null;
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
                wasMute = false;
                sound.Play();
                

            }

            if (previousColliderObject == null ^ (previousColliderObject != null && previousColliderObject != colliderObject))
            {
                if (previousColliderObject!=null && previousColliderObject.CompareTag("PressurePlate"))
                {
                    previousColliderObject.GetComponent<PressurePlate>().CollisionExited();
                }
                previousColliderObject = colliderObject;
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
                            speaker.mute = true;
                        }
                        break;
                    case "PressurePlate":
                        colliderObject.GetComponent<PressurePlate>().CollisionDetected();
                        break;

                }


            }


        }
        else
        {
            if (colliderObject!=null && colliderObject.CompareTag("PressurePlate"))
            {
                colliderObject.GetComponent<PressurePlate>().CollisionExited();
            }
            colliderObject = null;


        }

    }


}