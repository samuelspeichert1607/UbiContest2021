using UnityEngine;
using System;
using System.Timers;
using UnityEngine.EventSystems;

public class PlayerControllerForTest : CustomController
{
    
    [SerializeField] private float maxPlayerSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private int jumpValue;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField]
    [Range(0.01f, 5)]
    private float airborneAcceleration;
    [SerializeField]
    private float landingTime;

    private CharacterController controller;
    private GameObject cam;
    private Vector3 playerSpeed;

    private ControllerManager controllerManager;
    private bool wasGrounded = true;
    private bool isLanding = false;

    private float eulerAngleX;
    private float yAxisRotationScope = 0.0f;
    private float xAxisRotationScope = 70.0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = transform.GetChild(0).gameObject;
        controller = GetComponent<CharacterController>();
        eulerAngleX = cam.transform.position.y;
        controllerManager = GetComponent<ControllerManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            float rotationY = controllerManager.GetRightAxisY();
            float verticalMotion = controllerManager.GetLeftAxisY();
            float horizontalMotion = controllerManager.GetLeftAxisX();

            //on limite la rotation
            if (CameraCanRotate(rotationY))
            {
                eulerAngleX -= rotationY * Time.deltaTime * rotationSpeed;
                cam.transform.localEulerAngles = new Vector3(eulerAngleX, 0, 0);
            }

            //on tourne le joueur selon l'axe x du joystick droit
            transform.Rotate(new Vector3(0, controllerManager.GetRightAxisX(), 0) * (Time.deltaTime * rotationSpeed), Space.World);
            if (controller.isGrounded)
            {
                if (isLanding)
                {
                    verticalMotion *= 0.5f;
                    horizontalMotion *= 0.5f;
                }

                if (!wasGrounded)
                {
                    StartLanding();
                }
                //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
                if (playerSpeed.y < -1)
                {
                    playerSpeed.y = -1;
                }

                if (controllerManager.GetButtonDown("Jump"))
                {
                    playerSpeed.y = jumpValue;
                }
                wasGrounded = true;
                MoveAtMaxSpeed(verticalMotion, horizontalMotion, Time.deltaTime);
            }
            else
            {
                if (wasGrounded)
                {
                    SetInitialJumpSpeed(verticalMotion, horizontalMotion);
                }
                playerSpeed.y += gravity * Time.deltaTime;
                AdjustAirborneSpeed(verticalMotion, horizontalMotion);
                Move(playerSpeed, Time.deltaTime);
                wasGrounded = false;
            }
            
        }
    }
    
    private bool CameraCanRotate(float rotationY)
    {
        xAxisRotationScope = 70;
        return (Mathf.Abs(eulerAngleX) < xAxisRotationScope) || 
               (eulerAngleX >= xAxisRotationScope && rotationY > yAxisRotationScope) ||
               (eulerAngleX <= -xAxisRotationScope && rotationY < yAxisRotationScope);
    }

    private void StartLanding()
    {
        isLanding = true;
        Invoke("EndLanding", landingTime);
    }

    private void EndLanding()
    {
        isLanding = false;
    }

    public override void Move(Vector3 speed, float timeElapsed)
    {
        controller.Move(speed * timeElapsed);
    }

    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        controller.Move(transform.forward * (verticalMotion * timeElapsed * maxPlayerSpeed));
        controller.Move(transform.right * (horizontalMotion * timeElapsed * maxPlayerSpeed));
        controller.Move(new Vector3(0, playerSpeed.y, 0) * Time.deltaTime);
    }
    
    private void AdjustAirborneSpeed(float verticalMotion, float horizontalMotion)
    {
        var transform1 = transform;
        Vector3 speedIncrement = transform1.right * (airborneAcceleration * Time.deltaTime * horizontalMotion);
        speedIncrement += transform1.forward * (airborneAcceleration * Time.deltaTime * verticalMotion);
        playerSpeed.x = CapAtMaxSpeed(playerSpeed.x + speedIncrement.x);
        playerSpeed.z = CapAtMaxSpeed(playerSpeed.z + speedIncrement.z);
    }

    private void SetInitialJumpSpeed(float verticalMotion, float horizontalMotion)
    {
        var transform1 = transform;
        Vector3 initialJumpSpeed = transform1.right * (maxPlayerSpeed * horizontalMotion);
        initialJumpSpeed += transform1.forward * (maxPlayerSpeed * verticalMotion);
        playerSpeed.x = initialJumpSpeed.x;
        playerSpeed.z = initialJumpSpeed.z;
    }

    private float CapAtMaxSpeed(float speedValue)
    {
        if (speedValue >= maxPlayerSpeed)
        {
            return maxPlayerSpeed;
        }
        if (speedValue <= -maxPlayerSpeed)
        {
            return -maxPlayerSpeed;
        }
        return speedValue;
    }

}
