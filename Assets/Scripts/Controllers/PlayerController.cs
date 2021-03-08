using Photon.Pun;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PlayerController : CustomController
{
    
    [SerializeField] private float maxPlayerSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private int jumpValue;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField]
    [Range(0.01f, 10)]
    private float airborneAcceleration;
    [SerializeField]
    private float landingTime;
    [SerializeField]
    private float jumpingImpulseTime;
    [SerializeField] 
    private float minimalFallingSpeedForLandingPhase;


    private CharacterController controller;
    private GameObject cam;
    private Vector3 playerSpeed;
    private PhotonView photonView;

    private ControllerManager controllerManager;

    private float eulerAngleX;
    private float yAxisRotationScope = 0.0f;
    private float xAxisRotationScope = 70.0f;
    private bool wasGrounded = true;
    private bool isLanding = false;
    private bool isInitiatingAJump = false;
    private bool mustPlayLandingPhase;
    
    private float jumpingStartTime;
    
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        cam = transform.GetChild(0).gameObject;
        cam.GetComponent<Camera>().enabled = photonView.IsMine;
        controller = GetComponent<CharacterController>();
        eulerAngleX = cam.transform.position.y;
        controllerManager = GetComponent<ControllerManager>();
        playerSpeed = new Vector3(0,-1,0);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        UpdateCameraRotation();
        
        float verticalMotion = controllerManager.GetLeftAxisY();
        float horizontalMotion = controllerManager.GetLeftAxisX();

        if (isInitiatingAJump)
        {
            UpdateJumpingImpulse();
        }
        
        if (controller.isGrounded)
        {
            if (isLanding)
            {
                verticalMotion *= 0.5f;
                horizontalMotion *= 0.5f;
            }

            if (!wasGrounded && mustPlayLandingPhase)
            {
                StartLanding();
            }
            
            //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
            if (playerSpeed.y < -1)
            {
                playerSpeed.y = -1;
            }

            if (canMove)
            {
                if (controllerManager.GetButtonDown("Jump") && !isInitiatingAJump)
                {
                    InitiateJumping();
                }

                wasGrounded = true;
                MoveOnGround(verticalMotion, horizontalMotion);
            }
        }
        else
        {
            UpdateIfMustPlayLandingPhase();
            if (wasGrounded)
            {
                SetInitialJumpHorizontalSpeed(verticalMotion, horizontalMotion);
            }
            playerSpeed.y += gravity * Time.deltaTime;
            AdjustAirborneSpeed(verticalMotion, horizontalMotion);
            Move(playerSpeed, Time.deltaTime);
            wasGrounded = false;
        }
    }

    private void UpdateIfMustPlayLandingPhase()
    {
        if (playerSpeed.y <= - minimalFallingSpeedForLandingPhase)
        {
            mustPlayLandingPhase = true;
        }
        else
        {
            mustPlayLandingPhase = false;
        }
    }

    private void InitiateJumping()
    {
        jumpingStartTime = Time.time;
        isInitiatingAJump = true;
        Invoke(nameof(CheckForShortJump), 2.0f * jumpingImpulseTime / 3.0f);
        Jump();
    }

    private void CheckForShortJump()
    {
        if (controllerManager.GetButton("Jump")) return;
        isInitiatingAJump = false;
    }
    
    private void UpdateJumpingImpulse()
    {
        float fractionOfImpulseCompleted = ( Time.time - jumpingStartTime) / jumpingImpulseTime;

        if (fractionOfImpulseCompleted >= 1)
        {
            playerSpeed.y = jumpValue;
            isInitiatingAJump = false;
        }
        else
        {
            playerSpeed.y = fractionOfImpulseCompleted * jumpValue;
        }
        
    }
    
    private void UpdateCameraRotation()
    {
        float rotationY = controllerManager.GetRightAxisY();
        float rotationX = controllerManager.GetRightAxisX();
        //on limite la rotation
        if (CanCameraRotate(rotationY))
        {
            eulerAngleX -= rotationY * Time.deltaTime * GlobalSettings.RotationSpeed;
            cam.transform.localEulerAngles = new Vector3(eulerAngleX, 0, 0);
        }

        //on tourne le joueur selon l'axe x du joystick droit
        transform.Rotate(new Vector3(0, rotationX, 0) * (Time.deltaTime * GlobalSettings.RotationSpeed), Space.World);
    }

    private bool CanCameraRotate(float rotationY)
    {
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
    
    private void MoveOnGround(float verticalMotion, float horizontalMotion)
    {
        if (verticalMotion == 0f)
        {
            if (horizontalMotion < 0f)
            {
                StrafeLeft();
            }
            else if (horizontalMotion > 0f)
            {
                StrafeRight();
            }
            else
            {
                Idle();
            }
        }
        else if (verticalMotion >= 0.99f)
        {
            Run();
        }
        else
        {
            Walk();
        }
        MoveAtMaxSpeed(verticalMotion, horizontalMotion, Time.deltaTime);
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

    private void SetInitialJumpHorizontalSpeed(float verticalMotion, float horizontalMotion)
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
    
    private void Idle()
    {
        animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void StrafeLeft()
    {
        animator.SetFloat("Speed", 2f, 0.1f, Time.deltaTime);
    }

    private void StrafeRight()
    {
        animator.SetFloat("Speed", 1.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        animator.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        animator.SetTrigger("Jump");
    }


}
