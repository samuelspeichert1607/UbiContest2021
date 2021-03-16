
using UnityEngine;

public class XboxController : IController
{
    public float GetAxis(string axis)
    {
        switch (axis)
        {
            case "LStickX":
                return GetLeftAxisX();
                break;
            case "LStickY":
                return GetLeftAxisY();
                break;
            case "RStickX":
                return GetRightAxisX();
                break;
            case "RStickY":
                return GetRightAxisY();
                break;
            case "RTrigger":
                return GetRTriggerAxis();
                break;
            case "LTrigger":
                return GetLTriggerAxis();
                break;
            default:
                return 0;
        }
    }

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
        return Input.GetAxis("RotateX");
    }

    public float GetRightAxisY()
    {
        return Input.GetAxis("RotateY");
    }

    public float GetLTriggerAxis()
    {
        return Input.GetAxis("LTrigger");
    }
    public float GetRTriggerAxis()
    {
        return Input.GetAxis("RTrigger");
    }
    public bool GetButtonDown(string button)
    {
        switch (button)
        {
            case "Jump":
                return Input.GetButtonDown("Jump");
            case "B":
                return Input.GetButtonDown("B");
            case "Start":
                return Input.GetButtonDown("Start");
            case "Submit":
                return Input.GetButtonDown("Submit");
            case "Cancel":
                return Input.GetButtonDown("Cancel");
            case "LBumper":
                return Input.GetButtonDown("LBumper");
            case "RBumper":
                return Input.GetButtonDown("RBumper");
            default:
                return false;
        }
    }
    
    public bool GetButton(string button)
    {
        switch (button)
        {
            case "Jump":
                return Input.GetButton("Jump");
            case "B":
                return Input.GetButton("B");
            case "Start":
                return Input.GetButton("Start");
            case "Submit":
                return Input.GetButton("Submit");
            case "Cancel":
                return Input.GetButton("Cancel");
            case "LBumper":
                return Input.GetButton("LBumper");
            case "RBumper":
                return Input.GetButton("RBumper");
            default:
                return false;
        }
    }
}
