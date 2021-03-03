using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : CustomController
{
    [SerializeField] private int movementSpeed;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    
    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        controller.Move(- transform.forward * (verticalMotion * timeElapsed * movementSpeed));
        controller.Move(- transform.right * (horizontalMotion * timeElapsed * movementSpeed));
    }
    

    public override void Move(Vector3 speed, float timeElapsed)
    {
        controller.Move(- transform.forward * (speed.z * timeElapsed));
        controller.Move(- transform.right * (speed.x * timeElapsed));
    }

}
