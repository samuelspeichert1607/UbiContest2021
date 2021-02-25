
using UnityEngine;

public class PS4Controller : IController
{

    public float GetLeftAxisX()
    {
        return Input.GetAxis("Horizontal");
    }
    
    public float GetLeftAxisY()
    {
        return Input.GetAxis("Vertical");
    }
    
    public float GetRightAxisX()
    {
        return Input.GetAxis("PS4RightX");
    }
    
    public float GetRightAxisY()
    {
        return -1 * Input.GetAxis("DpadX");
    }
    
    public float GetLTriggerAxis()
    {
        return Input.GetAxis("RotateX");
    }
    
    public float GetRTriggerAxis()
    {
        return Input.GetAxis("RotateY");
    }

    public bool GetButtonDown(string button)
    {
        switch (button)
        {
            case "Jump" :
                return Input.GetButtonDown("B");
            case "B":
                return Input.GetButtonDown("X");
            case "Start":
                return Input.GetButtonDown("Button9");
            case "Submit":
                return Input.GetButtonDown("B");
            case "Cancel":
                return Input.GetButtonDown("X");
            case "LBumper":
                return Input.GetButtonDown("LBumper");
            case "RBumper":
                return Input.GetButtonDown("RBumper");
            default:
                return false;
        }
    }
}
