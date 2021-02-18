

public class ControllerPicker
{
    private IController previousController;
    private string previousControllerName;
    
    public ControllerPicker() {}

    public IController PickController(string controllerName)
    {
        // Peut-être à ajuster pour les manettes 3rd party qui fonctionnent comme des manettes de xbox
        if (controllerName == "Controller (Xbox One For Windows)")
        {
            previousControllerName = controllerName;
            previousController = new XboxController();
        }

        else if (controllerName == "Wireless Controller")
        {
            previousControllerName = controllerName;
            previousController = new PS4Controller();
        }
        return previousController;
    }

    public bool IsDifferentController(string controllerName)
    {
        return previousControllerName != controllerName;
    }
}
