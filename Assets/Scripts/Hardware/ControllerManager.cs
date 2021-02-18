using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControllerManager
{
    private static IController currentController = PickController();
    
    public static float GetLeftAxisX()
    {
        return currentController.GetLeftAxisX();
    }

    public static float GetLeftAxisY()
    {
        return currentController.GetLeftAxisY();
    }
    private static IController PickController()
    {
        string newController = Input.GetJoystickNames()[0];

            // Peut-être à ajuster pour les manettes 3rd party qui fonctionnent comme des manettes de xbox
        if (newController == "Controller (Xbox One For Windows)")
        {
            return new XboxController();
        }

        else if (newController == "Wireless Controller")
        {
            return new PS4Controller();
        }
        else
        {
            return null;
        }
    }
}
