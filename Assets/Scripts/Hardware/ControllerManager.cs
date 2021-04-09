using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum ControllerType
{
    XboxController,
    PS4Controller
};

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private ButtonLayout XboxLayout;
    [SerializeField] private ButtonLayout Ps4Layout;
    private IController userController;
    private string currentController;
    private ButtonLayout currentButtonLayout;

    public void Start()
    {
        PickController();
    }

    public float GetAxis(string axis)
    {
        return userController.GetAxis(axis);
    }

    public float GetLeftAxisX()
    {
        return userController.GetLeftAxisX();
    }

    public float GetRightAxisX()
    {
        return userController.GetRightAxisX();
    }

    public float GetRightAxisY()
    {
        return userController.GetRightAxisY();
    }

    public bool GetButtonDown(string button)
    {
        return userController.GetButtonDown(button);
    }
    
    public bool GetButton(string button)
    {
        return userController.GetButton(button);
    }

    public float GetLeftAxisY()
    {
        return userController.GetLeftAxisY();
    }
    private void PickController()
    {
        string newController = Input.GetJoystickNames()[0];
        if (IsDifferentController(newController))
        {
            // Peut-être à ajuster pour les manettes 3rd party qui fonctionnent comme des manettes de xbox
            if (newController == "Controller (Xbox One For Windows)" || newController == "Afterglow Gamepad for Xbox 360"
            || (newController == "Controller (XBOX 360 For Windows"))
            {
                userController = new XboxController();
                currentButtonLayout = XboxLayout;
            }

            else if (newController == "Wireless Controller")
            {
                userController = new PS4Controller();
                currentButtonLayout = Ps4Layout;
            }
            else
            {
                userController = new XboxController();
                currentButtonLayout = XboxLayout;
            }
            currentController = newController;
        }
    }

    private bool IsDifferentController(string newController)
    {
        return (currentController != newController);
    }
    
    public ControllerType GetControllerType()
    {
        string controller = Input.GetJoystickNames()[0];
        if (controller == "Controller (Xbox One For Windows)" || controller == "Afterglow Gamepad for Xbox 360"
                                                              || (controller == "Controller (XBOX 360 For Windows"))
        {
            return ControllerType.XboxController;
        }

        else if (controller == "Wireless Controller")
        {
            return ControllerType.PS4Controller;
        }
        return ControllerType.XboxController;
    }

    public ButtonLayout CurrentButtonLayout => currentButtonLayout;
}
