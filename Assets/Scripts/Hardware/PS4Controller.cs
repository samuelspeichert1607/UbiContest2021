
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PS4Controller : IController
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
        return Mathf.Clamp(-1 * Input.GetAxis("RotateY"), 0, 1.0f);
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
    
    public bool GetButton(string button)
    {
        switch (button)
        {
            case "Jump" :
                return Input.GetButton("B");
            case "B":
                return Input.GetButton("X");
            case "Start":
                return Input.GetButton("Button9");
            case "Submit":
                return Input.GetButton("B");
            case "Cancel":
                return Input.GetButton("X");
            case "LBumper":
                return Input.GetButton("LBumper");
            case "RBumper":
                return Input.GetButton("RBumper");
            default:
                return false;
        }
    }
}
