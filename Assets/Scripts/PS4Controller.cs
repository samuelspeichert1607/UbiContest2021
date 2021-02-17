
using UnityEngine;

public class PS4Controller : IController
{
    public PS4Controller() {}

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
        return Input.GetAxis("Mouse ScrollWheel");
    }
    public float GetRightAxisY()
    {
        return Input.GetAxis("DpadX");
    }
    public bool GetButtonDown(string button)
    {
        switch (button)
        {
            case "Jump" :
                return Input.GetButtonDown("B");
            default:
                return false;
        }
    }
}
