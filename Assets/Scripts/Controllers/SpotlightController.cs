using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : CustomController
{
    [SerializeField] private int movementSpeed;
    private CharacterController controller;
    private Vector3 previousPosition;
    private AudioSource movingSound;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
        controller = GetComponent<CharacterController>();
        movingSound = GetComponent<AudioSource>();
    }
    
    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
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
        controller.Move( transform.forward * (speed.z * timeElapsed));
        controller.Move( transform.right * (speed.x * timeElapsed));
    }

}
