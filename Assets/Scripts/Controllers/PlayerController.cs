using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int PlayerSpeed;
    public int RotationSpeed;
    public int jumpValue;
    public float gravity = -9.81f;
    private CharacterController controller;
    private float velocityY;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //on tourne le joueur selon l'axe x du joystick droit
        transform.Rotate(new Vector3(0, Input.GetAxis("RotateX"), 0) * Time.deltaTime * RotationSpeed, Space.World);


        if (controller.isGrounded)
        {
            //je sais que c'est bizarre mais, si je reset la velocite a 0, le controller.isGrounded ne fonctionne pas -_-
            if (velocityY < -1)
            {
                velocityY = -1;
            }
            if (Input.GetButtonDown("Jump"))
            {
                velocityY = jumpValue;
            }
        }
        else
        {
            velocityY += gravity * Time.deltaTime;
        }

        //le joueur se deplace
        controller.Move(transform.forward * -Input.GetAxis("Vertical") * Time.deltaTime * PlayerSpeed);
        controller.Move(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed);
        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);

    }

}
