using UnityEngine;


public class CollisionController : MonoBehaviour
{
    [SerializeField] private float raycastRange = 0.1f;
    [SerializeField] private GameObject bottomObject;

    private AudioSource speaker;
    private AudioSource sound;

    private GameObject previousObject = null;
    private void Start()
    {
        speaker = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
    }
    
    void Update()
    {

        Vector3 bottomCenterPos = bottomObject.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(bottomCenterPos, Vector3.down, out hit, raycastRange))
        {
            GameObject obj = hit.collider.gameObject;

            if ((previousObject == null || previousObject != obj))
            {

                CollisionManagement(obj);

                if (previousObject != null)
                {
                    CollisionExitedManagement(previousObject);
                }
                previousObject = obj;
            }
        }
        else
        {
            if (previousObject != null)
            {
                CollisionExitedManagement(previousObject);
            }
            previousObject = null;
        }

    }

    private void CollisionManagement(GameObject colliderObject)
    {
        switch (colliderObject.tag)
        {
            case "MarelleTile":
                colliderObject.transform.parent.GetComponent<ParentTile>().CollisionDetected(colliderObject);
                break;
            case "MutePlateforme":
                sound = colliderObject.GetComponent<AudioSource>();
                Debug.Log("In mute zone");
                sound.Play();
                speaker.mute = true;
                break;
            case "PressurePlate":
                colliderObject.GetComponent<PressurePlate>().CollisionDetected();
                break;
        }
    }

    private void CollisionExitedManagement(GameObject colliderObject)
    {
        switch (colliderObject.tag)
        {
            case "PressurePlate":
                colliderObject.GetComponent<PressurePlate>().CollisionExited();
                break;
            case "MutePlateforme":
                speaker.mute = false;
                Debug.Log("Out mute zone");
                sound.Play();
                break;
        }
    }



}


