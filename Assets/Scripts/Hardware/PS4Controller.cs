
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
    public bool GetButtonDown(string button)
    {
        switch (button)
        {
            case "Jump" :
                return Input.GetButtonDown("B");
            case "B":
                return Input.GetButtonDown("X");
            case "Button7":
                return Input.GetButtonDown("Button9");
            case "Submit":
                return Input.GetButtonDown("B");
            case "Cancel":
                return Input.GetButtonDown("X");
            default:
                return false;
        }
    }
}
