using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayComponents : MonoBehaviour
{
    public Vector3 positionVec;
    public GameObject previousObject;
    
    public RayComponents(Vector3 vec )
    {
        positionVec = vec;
        previousObject = null;
    }
}

public class CollisionController : MonoBehaviour
{
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;

    private AudioSource speaker;
    private AudioSource sound;

    private Vector3 playerSize;

    private RayComponents[] loopArray;
    private void Start()
    {
        speaker = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        playerSize = GetComponent<BoxCollider>().bounds.size;

        RayComponents[] temp = { new RayComponents(new Vector3()) , new RayComponents(new Vector3(0, 0, -playerSize.z)) ,
            new RayComponents(new Vector3(0, 0, playerSize.z)) , new RayComponents(new Vector3(playerSize.x, 0,0)),
        new RayComponents(new Vector3(-playerSize.x, 0,0))};

        loopArray = temp; //sinon j<ai plein d<erreurs..
    }



    void Update()
    {
        Vector3 bottomCenterPos = bottomObject.transform.position;
        List<GameObject> objectsInCollision = new List<GameObject>();
        List<GameObject> objectsExited = new List<GameObject>();
        foreach (RayComponents components in loopArray)
        {
            Vector3 postemp;
            if (components.positionVec.z != 0)
            {
                postemp = components.positionVec.z * transform.forward;
            }
            else
            {
                postemp = components.positionVec.x * transform.right;
            }
            RaycastHit hit;

            if (Physics.Raycast(bottomCenterPos + postemp, Vector3.down, out hit, raycastRange))
            {
                GameObject obj = hit.collider.gameObject;

                if ((components.previousObject == null || components.previousObject != obj) && !objectsInCollision.Contains(obj))
                {

                    CollisionManagement(obj,components.positionVec);

                    if (components.previousObject != null && !(objectsInCollision.Contains(components.previousObject) || objectsExited.Contains(components.previousObject)))
                    {
                        objectsExited.Add(components.previousObject);
                        CollisionExitedManagement(components.previousObject, components.positionVec);
                    }
                    components.previousObject = obj;
                }
                objectsInCollision.Add(obj);
            }
            else
            {
                if (components.previousObject != null && !(objectsInCollision.Contains(components.previousObject) || objectsExited.Contains(components.previousObject)))
                {
                    objectsExited.Add(components.previousObject);
                    CollisionExitedManagement(components.previousObject, components.positionVec);
                }
                components.previousObject = null;
            }
        }

    }

    private void CollisionManagement(GameObject colliderObject,Vector3 vec)
    {
        switch (colliderObject.tag)
        {
            case "MarelleTile":
                colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);
                break;
            case "MutePlateforme":
                sound = colliderObject.GetComponent<AudioSource>();
                sound.Play();
                speaker.mute = true;
                break;
            case "PressurePlate":
                if(vec==Vector3.zero)
                {
                    colliderObject.GetComponent<PressurePlate>().CollisionDetected();
                }
                
                break;

        }
    }

    private void CollisionExitedManagement(GameObject colliderObject,Vector3 vec)
    {
        switch (colliderObject.tag)
        {
            case "PressurePlate":
                if (vec == Vector3.zero)
                {
                    colliderObject.GetComponent<PressurePlate>().CollisionExited();
                }
                    
                break;
            case "MutePlateforme":
                speaker.mute = false;
                sound.Play();
                break;
        }
    }



}


