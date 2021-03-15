using Photon.Pun;
using UnityEngine;

public class SpotlightController : CustomController
{
    [SerializeField] private int movementSpeed;
    private CharacterController controller;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
        controller = GetComponent<CharacterController>();
    }
    
    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        photonView.RequestOwnership();
        controller.Move(transform.forward * (verticalMotion * timeElapsed * movementSpeed));
        controller.Move(transform.right * (horizontalMotion * timeElapsed * movementSpeed));
    }
    

    public override void Move(Vector3 speed, float timeElapsed)
    {
        photonView.RequestOwnership();
        controller.Move( transform.forward * (speed.z * timeElapsed));
        controller.Move( transform.right * (speed.x * timeElapsed));
    }

}
