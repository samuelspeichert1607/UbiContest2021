using Photon.Pun;
using UnityEngine;
using System;

public class PlayerController : CustomController
{

    [SerializeField] private int playerSpeed;
    [SerializeField] private int RotationSpeed;
    [SerializeField] private int jumpValue;
    [SerializeField] private float gravity = -9.81f;
    private CharacterController controller;
    private GameObject cam;
    private float velocityY = -1;
    private PhotonView photonView;
    
    private IController userController;
    private ControllerPicker controllerPicker;

    private float eulerAngleX;

    // Start is called before the first frame update
    void Start()
    {
        // photonView = GetComponent<PhotonView>();
        cam = transform.GetChild(0).gameObject;
        // cam.GetComponent<Camera>().enabled = photonView.IsMine;
        controller = GetComponent<CharacterController>();
        eulerAngleX = cam.transform.position.y;
        controllerPicker = new ControllerPicker();
    }

    // Update is called once per frame
    void Update()
    {
        // if (!photonView.IsMine) return;
        
        string controllerUsed = Input.GetJoystickNames()[0];
        PickController(controllerUsed);

        float rotationY = userController.GetRightAxisY();

        //on limite la rotation
        if (canMove)
        {
            //on limite la rotation
            if ((Mathf.Abs(eulerAngleX) < 90) || (eulerAngleX >= 90 && rotationY > 0) ||
                (eulerAngleX <= -90 && rotationY < 0))
            {
                eulerAngleX -= rotationY * Time.deltaTime * RotationSpeed;
                cam.transform.localEulerAngles = new Vector3(eulerAngleX, 0, 0);
            }

            //on tourne le joueur selon l'axe x du joystick droit
            transform.Rotate(new Vector3(0, userController.GetRightAxisX(), 0) * (Time.deltaTime * RotationSpeed), Space.World);

            if (controller.isGrounded)
            {
                //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
                if (velocityY < -1)
                {
                    velocityY = -1;
                }

                if (userController.GetButtonDown("Jump"))
                {
                    velocityY = jumpValue;
                }
            }
            else
            {
                velocityY += gravity * Time.deltaTime;
            }

            //le joueur se deplace
            Move(userController.GetLeftAxisY(), userController.GetLeftAxisX(), Time.deltaTime);
        }
    }

    public override void Move(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        controller.Move(transform.forward * (verticalMotion * timeElapsed * playerSpeed));
        controller.Move(transform.right * (horizontalMotion * timeElapsed * playerSpeed));
        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
    }

    private void PickController(string controllerUsed)
    {
        if (controllerPicker.IsDifferentController(controllerUsed))
        {
            userController = controllerPicker.PickController(controllerUsed);
        }
    }
    
}
