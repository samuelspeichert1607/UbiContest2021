
using UnityEngine;

public class XboxController : IController
{

    public XboxController() { }

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
            default:
                return false;
        }
    }
}
