using Photon.Pun;
using UnityEngine;

public class PlayerController : CustomController
{
    
    [SerializeField] private float maxPlayerSpeed;
    [SerializeField] private int jumpValue;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] [Range(0.01f, 10)] private float airborneAcceleration;
    [SerializeField] private float landingTime = 0.1f;
    [SerializeField] private float minimalJumpingAscensionTime = 0.25f;
    [SerializeField] private float onJumpingReleaseDecelerationFactor = 15f;
    [SerializeField] private float onAirborneNoMotionDecelerationFactor = 5f;
    [SerializeField] private float minimalFallingSpeedForLandingPhase = 5f;
    [SerializeField] private AudioClip jumpingSounds;
    [SerializeField] private AudioClip landingSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject deathCam;

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
    private bool _isInJumpingAscensionPhase;
    private bool _mustPlayLandingPhase;

    private float _jumpingStartTime;
    
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Jump1 = Animator.StringToHash("Jump");
    private static readonly int Emote = Animator.StringToHash("Emote");
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedZ = Animator.StringToHash("SpeedZ");
    private static readonly int Falling = Animator.StringToHash("Falling");
    private static readonly int Landed = Animator.StringToHash("Landed");
    
    private const float WalkThreshold = 0.5f;
    private const float RunThreshold = 0.75f;
    private const float DiagonalThresholdX = 0.2f;
    private const float DiagonalThresholdZ = 0.35f;
    private AudioListener _audioListener;
    private GameObject _networkManager;
    private static readonly int IsDying = Animator.StringToHash("isDying");

    // Start is called before the first frame update
    void Start()
    {
        _photonView = GetComponent<PhotonView>();
        _networkManager = GameObject.Find("NetworkManager");
        _camera = transform.GetChild(0).gameObject;
        _camera.GetComponent<Camera>().enabled = _photonView.IsMine;
        _controller = GetComponent<CharacterController>();
        _eulerAngleX = _camera.transform.position.y;
        _controllerManager = GetComponent<ControllerManager>();
        _playerSpeed = new Vector3(0,-1,0);
        _animator = GetComponentInChildren<Animator>();
        _audioListener = GetComponent<AudioListener>();
        _audioListener.enabled = _photonView.IsMine;
        
        deathCam.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_photonView.IsMine) return;

        UpdateCameraRotation();
        
        float verticalMotion = _controllerManager.GetLeftAxisY();
        float horizontalMotion = _controllerManager.GetLeftAxisX();

        if (_isInJumpingAscensionPhase)
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

            if (!_wasGrounded)
            {
                if (_mustPlayLandingPhase)
                {
                    _photonView.RPC("StartLanding", RpcTarget.All);
                }
                else
                {
                    isInCriticalMotion = false;
                }
            }

            //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
            if (_playerSpeed.y < -1)
            {
                _playerSpeed.y = -1;
            }

            if (canMove)
            {
                if (_controllerManager.GetButtonDown("Jump") && !_isInJumpingAscensionPhase)
                {
                    _photonView.RPC("InitiateJumping", RpcTarget.All);
                }
                else if (_controllerManager.GetButtonDown("RBumper"))
                {
                    PlayEmote();
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
                isInCriticalMotion = true;
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
            _eulerAngleX -= rotationY * Time.deltaTime * GlobalSettings.RotationSpeed;
            _camera.transform.localEulerAngles = new Vector3(_eulerAngleX, 0, 0);
        }

        //on tourne le joueur selon l'axe x du joystick droit
        transform.Rotate(new Vector3(0, rotationX, 0) * (Time.deltaTime * GlobalSettings.RotationSpeed), Space.World);
    }

    private bool CanCameraRotate(float rotationY)
    {
        return (Mathf.Abs(_eulerAngleX) < xAxisRotationScope) || 
               (_eulerAngleX >= xAxisRotationScope && rotationY > yAxisRotationScope) ||
               (_eulerAngleX <= -xAxisRotationScope && rotationY < yAxisRotationScope);
    }
    
    private void MoveOnGround(float verticalMotion, float horizontalMotion)
    {
        if (verticalMotion == 0f && horizontalMotion == 0f)
        {
            Idle();
        }
        
        else if (verticalMotion <= 0.1f && verticalMotion >= -0.1f)
        {
            if (horizontalMotion < 0f && horizontalMotion > -0.75f)
            {
                StrafeLeft();
            }
            else if (horizontalMotion <= -0.75f)
            {
                StrafeLeftRun();
            }
            else if (horizontalMotion > 0f && horizontalMotion < 0.75f)
            {
                StrafeRight();
            }
            else if (horizontalMotion >= 0.75f)
            {
                StrafeRightRun();
            }
        }
        else if(horizontalMotion >= -0.1f && horizontalMotion <= 0.1f)
        {
            if (verticalMotion < 0 && verticalMotion > -0.75f)
            {
                Backwards();
            }
            else if (verticalMotion <= -0.75f)
            {
                BackwardsRun();
            }
            else if (verticalMotion > 0 && verticalMotion < 0.75f)
            {
                Walk();
            }
            else if (verticalMotion >= 0.75f)
            {
                Run();
            }
        }
        else if (verticalMotion >= 0.5f)
        {
            if (horizontalMotion > 0.5f)
            {
                DiagonalRight();
            }
            else if (horizontalMotion < -0.5f)
            {
                DiagonalLeft();
            }
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

    [PunRPC]
    private void InitiateJumping()
    {
        audioSource.PlayOneShot(jumpingSounds, 0.7f);
        _jumpingStartTime = Time.time;
        _isInJumpingAscensionPhase = true;
        _playerSpeed.y = jumpValue;
        StartJump();
    }
    
    private void UpdateJumpingImpulse()
    {
        if (minimalJumpingAscensionTime < Time.time - _jumpingStartTime)
        {
            if (!_controllerManager.GetButton("Jump"))
            {
                float initialSpeedMomentum = 1 - (onJumpingReleaseDecelerationFactor * Time.deltaTime);
                _playerSpeed.y *= initialSpeedMomentum;
            }
        }

        if (_playerSpeed.y <= 0)
        {
            _isInJumpingAscensionPhase = false;
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

    [PunRPC]
    private void StartLanding()
    {
        audioSource.PlayOneShot(landingSounds, 0.7f);
        _isLanding = true;
        Invoke("EndLanding", landingTime);
    }

    private void EndLanding()
    {
        _isLanding = false;
        isInCriticalMotion = false;
    }

    private void Idle()
    {
        _animator.SetFloat(SpeedX, 0, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        _animator.SetFloat(SpeedX, 0, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, WalkThreshold, 0.1f, Time.deltaTime);
    }

    private void StrafeLeft()
    {
        _animator.SetFloat(SpeedX, -WalkThreshold, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, 0, 0.1f, Time.deltaTime);
    }
    
    private void StrafeLeftRun()
    {
        _animator.SetFloat(SpeedX, -RunThreshold, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, 0f, 0.1f, Time.deltaTime);
    }

    private void StrafeRight()
    {
        _animator.SetFloat(SpeedX, WalkThreshold, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, 0, 0.1f, Time.deltaTime);
    }

    private void StrafeRightRun()
    {
        _animator.SetFloat(SpeedX, RunThreshold, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, 0, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        _animator.SetFloat(SpeedX, 0, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, RunThreshold, 0.1f, Time.deltaTime);
    }

    private void DiagonalRight()
    {
        _animator.SetFloat(SpeedX, DiagonalThresholdX, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, DiagonalThresholdZ, 0.1f, Time.deltaTime);
    }

    private void DiagonalLeft()
    {
        _animator.SetFloat(SpeedX, -DiagonalThresholdX, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, DiagonalThresholdZ, 0.1f, Time.deltaTime);
    }

    private void Backwards()
    {
        _animator.SetFloat(SpeedX, 0, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, -WalkThreshold, 0.1f, Time.deltaTime);
    }

    private void BackwardsRun()
    {
        _animator.SetFloat(SpeedX, 0, 0.1f, Time.deltaTime);
        _animator.SetFloat(SpeedZ, -RunThreshold, 0.1f, Time.deltaTime);
    }
    
    private void Jump()
    {
        _animator.SetTrigger(Jump1);
    }
    
    private void PlayEmote()
    {
        isInCriticalMotion = true;
        _animator.SetTrigger(Emote);
        Invoke(nameof(EndEmote),1.7f);
    }
    
    private void StartJump()
    {
        _animator.SetFloat(Speed, 0f, 0.1f, Time.deltaTime);
        _animator.SetTrigger(Jump1);
    }
    
    public void Disconnect(int indexSceneToLoad)
    {
        _photonView.RPC(nameof(RPCDisconnect), RpcTarget.AllViaServer, indexSceneToLoad);
    }

    [PunRPC]
    private void RPCDisconnect(int indexSceneToLoad)
    {
        Debug.Log("Logging out");
        
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel(indexSceneToLoad);
    }
    
    
    private void MidJump()
    {
        _animator.SetTrigger(Falling);
    }
    
    private void EndJump()
    {
        _animator.SetTrigger(Landed);

    }
    
    public void ChangeCanMove()
    {
        canMove = !canMove;
    }

    public override void PlayDeathAnimation()
    {
        _camera.SetActive(false);
        deathCam.SetActive(true);
        _animator.SetBool(IsDying, true);
    }
    
    
    private void EndEmote()
    {
        isInCriticalMotion = false;
    }
}
