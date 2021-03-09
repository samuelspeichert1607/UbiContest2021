using Photon.Pun;
using UnityEngine;

public class PlayerController : CustomController
{
    
    [SerializeField] private float maxPlayerSpeed;
    [SerializeField] private int rotationSpeed;
    [SerializeField] private int jumpValue;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] [Range(0.01f, 10)] private float airborneAcceleration;
    [SerializeField] private float landingTime = 0.1f;
    [SerializeField] private float minimalJumpTime = 0.25f;
    [SerializeField] private float onJumpingReleaseDecelerationFactor = 15f;
    [SerializeField] private float onAirborneNoMotionDecelerationFactor = 5f;
    [SerializeField] private float minimalFallingSpeedForLandingPhase = 5f;


    private CharacterController _controller;
    private GameObject _camera;
    private Vector3 _playerSpeed;
    private PhotonView _photonView;

    private ControllerManager _controllerManager;

    private float _eulerAngleX;
    private float yAxisRotationScope = 0.0f;
    private float xAxisRotationScope = 70.0f;
    private bool _wasGrounded = true;
    private bool _isLanding;
    private bool _isInitiatingAJump;
    private bool _mustPlayLandingPhase;
    
    private float _jumpingStartTime;
    
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump1 = Animator.StringToHash("Jump");

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _camera = transform.GetChild(0).gameObject;
        _camera.GetComponent<Camera>().enabled = _photonView.IsMine;
        _controller = GetComponent<CharacterController>();
        _eulerAngleX = _camera.transform.position.y;
        _controllerManager = GetComponent<ControllerManager>();
        _playerSpeed = new Vector3(0,-1,0);
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine) return;

        UpdateCameraRotation();
        
        float verticalMotion = _controllerManager.GetLeftAxisY();
        float horizontalMotion = _controllerManager.GetLeftAxisX();

        if (_isInitiatingAJump)
        {
            UpdateJumpingImpulse();
        }
        
        if (_controller.isGrounded)
        {
            if (_isLanding)
            {
                verticalMotion *= 0.5f;
                horizontalMotion *= 0.5f;
            }

            if (!_wasGrounded && _mustPlayLandingPhase)
            {
                StartLanding();
            }
            
            //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
            if (_playerSpeed.y < -1)
            {
                _playerSpeed.y = -1;
            }

            if (canMove)
            {
                if (_controllerManager.GetButtonDown("Jump") && !_isInitiatingAJump)
                {
                    InitiateJumping();
                }

                _wasGrounded = true;
                MoveOnGround(verticalMotion, horizontalMotion);
            }
        }
        else
        {
            UpdateIfMustPlayLandingPhase();
            if (_wasGrounded)
            {
                SetInitialJumpHorizontalSpeed(verticalMotion, horizontalMotion);
            }
            _playerSpeed.y += gravity * Time.deltaTime;
            AdjustAirborneSpeed(verticalMotion, horizontalMotion);
            Move(_playerSpeed, Time.deltaTime);
            _wasGrounded = false;
        }
    }

    private void UpdateIfMustPlayLandingPhase()
    {
        if (_playerSpeed.y <= - minimalFallingSpeedForLandingPhase)
        {
            _mustPlayLandingPhase = true;
        }
        else
        {
            _mustPlayLandingPhase = false;
        }
    }
    
    private void UpdateCameraRotation()
    {
        float rotationY = _controllerManager.GetRightAxisY();
        float rotationX = _controllerManager.GetRightAxisX();
        //on limite la rotation
        if (CanCameraRotate(rotationY))
        {
            _eulerAngleX -= rotationY * Time.deltaTime * rotationSpeed;
            _camera.transform.localEulerAngles = new Vector3(_eulerAngleX, 0, 0);
        }

        //on tourne le joueur selon l'axe x du joystick droit
        transform.Rotate(new Vector3(0, rotationX, 0) * (Time.deltaTime * rotationSpeed), Space.World);
    }

    private bool CanCameraRotate(float rotationY)
    {
        return (Mathf.Abs(_eulerAngleX) < xAxisRotationScope) || 
               (_eulerAngleX >= xAxisRotationScope && rotationY > yAxisRotationScope) ||
               (_eulerAngleX <= -xAxisRotationScope && rotationY < yAxisRotationScope);
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
        _controller.Move(speed * timeElapsed);
    }

    public override void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        _controller.Move(transform.forward * (verticalMotion * timeElapsed * maxPlayerSpeed));
        _controller.Move(transform.right * (horizontalMotion * timeElapsed * maxPlayerSpeed));
        _controller.Move(new Vector3(0, _playerSpeed.y, 0) * Time.deltaTime);
    }
    

    private void SetInitialJumpHorizontalSpeed(float verticalMotion, float horizontalMotion)
    {
        var transform1 = transform;
        Vector3 initialJumpSpeed = transform1.right * (maxPlayerSpeed * horizontalMotion);
        initialJumpSpeed += transform1.forward * (maxPlayerSpeed * verticalMotion);
        _playerSpeed.x = initialJumpSpeed.x;
        _playerSpeed.z = initialJumpSpeed.z;
    }
    
    private void InitiateJumping()
    {
        _jumpingStartTime = Time.time;
        _isInitiatingAJump = true;
        _playerSpeed.y = jumpValue;
        Jump();
    }
    
    private void UpdateJumpingImpulse()
    {
        if (minimalJumpTime < Time.time - _jumpingStartTime)
        {
            if (!_controllerManager.GetButton("Jump"))
            {
                float initialSpeedMomentum = 1 - (onJumpingReleaseDecelerationFactor * Time.deltaTime);
                _playerSpeed.y *= initialSpeedMomentum;
            }
        }

        if (_playerSpeed.y <= 0)
        {
            _isInitiatingAJump = false;
        }
    }
    private void AdjustAirborneSpeed(float verticalMotion, float horizontalMotion)
    {
        var transform1 = transform;
        if (verticalMotion != 0 || horizontalMotion != 0)
        {
            Vector3 speedIncrement = transform1.right * (airborneAcceleration * Time.deltaTime * horizontalMotion);
            speedIncrement += transform1.forward * (airborneAcceleration * Time.deltaTime * verticalMotion);
            _playerSpeed = CapPlayerSpeed(_playerSpeed + speedIncrement);
        }
        else
        {
            float initialSpeedMomentum = 1 - (airborneAcceleration * Time.deltaTime /onAirborneNoMotionDecelerationFactor); 
            _playerSpeed.x *= initialSpeedMomentum;
            _playerSpeed.z *= initialSpeedMomentum;
        }
    }

    private Vector3 CapPlayerSpeed(Vector3 incrementedPlayerSpeed)
    {
        Vector2 planeSpeed = new Vector2(incrementedPlayerSpeed.x, incrementedPlayerSpeed.z);
        if (planeSpeed.magnitude  > maxPlayerSpeed)
        {
            Vector2 capedSpeed = planeSpeed.normalized * maxPlayerSpeed;
            incrementedPlayerSpeed.x = capedSpeed.x;
            incrementedPlayerSpeed.z = capedSpeed.y;
        }
        return incrementedPlayerSpeed;
    }
    
    private void StartLanding()
    {
        _isLanding = true;
        Invoke("EndLanding", landingTime);
    }

    private void EndLanding()
    {
        _isLanding = false;
        
    }

    private void Idle()
    {
        _animator.SetFloat(Speed, 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        _animator.SetFloat(Speed, 0.5f, 0.1f, Time.deltaTime);
    }

    private void StrafeLeft()
    {
        _animator.SetFloat(Speed, 2f, 0.1f, Time.deltaTime);
    }

    private void StrafeRight()
    {
        _animator.SetFloat(Speed, 1.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        _animator.SetFloat(Speed, 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        _animator.SetTrigger(Jump1);
    }

}
