
using UnityEngine;

public class XboxController : IController
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
        return Input.GetAxis("RotateX");
    }

    public float GetRightAxisY()
    {
        return Input.GetAxis("RotateY");
    }

    public bool GetButtonDown(string button)
    {
        switch (button)
        {
            case "Jump":
                return Input.GetButtonDown("Jump");
            case "B":
                return Input.GetButtonDown("B");
            case "XboxStart":
                return Input.GetButtonDown("XboxStart");
            case "Submit":
                return Input.GetButtonDown("Submit");
            case "Cancel":
                return Input.GetButtonDown("Cancel");
            default:
                return false;
        }
    }
}
