using Photon.Pun;
using UnityEngine;

public class SpotlightController : CustomController
{
    [SerializeField] private int movementSpeed;
    private CharacterController controller;
    private Vector3 previousPosition;
    private AudioSource movingSound;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        previousPosition = transform.position;
        controller = GetComponent<CharacterController>();
        movingSound = GetComponent<AudioSource>();
    }
    
    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        photonView.RequestOwnership();
        controller.Move(transform.forward * (verticalMotion * timeElapsed * movementSpeed));
        controller.Move(transform.right * (horizontalMotion * timeElapsed * movementSpeed));
        if (transform.position != previousPosition && !movingSound.isPlaying)
        {
            movingSound.Play();
        }

        previousPosition = transform.position;
    }
    

    public override void Move(Vector3 speed, float timeElapsed)
    {
        photonView.RequestOwnership();
        controller.Move( transform.forward * (speed.z * timeElapsed));
        controller.Move( transform.right * (speed.x * timeElapsed));
    }

}
