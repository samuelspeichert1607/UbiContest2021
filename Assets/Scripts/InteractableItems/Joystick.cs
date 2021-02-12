using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractableItem;
using static CustomController;

public class Joystick : InteractableItem
{
    
    [SerializeField] private CustomController target;
    [SerializeField] private TextRenderer textRenderer;

    // Update is called once per frame
    void Update()
    {
        CheckIfAPlayerIsInRange();
        if (hasPlayerInRange)
        {
            OnPlayerInRange();
        }

        if (isInteractedWith)
        {
            target.Move(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Time.deltaTime);
            //Animation of the joystick would go here
        }
        
    }

    public override void OnInteract()
    {
        base.OnInteract();

        isInteractedWith = !isInteractedWith;
    }

    public override void OnPlayerInRange()
    {
        textRenderer.ShowInfo(InteractPreButtonText + " " + InteractButtonName + " " + InteractPostButtonText);
        
        if (Input.GetButton(InteractButtonName))
        {
            isInteractedWith = true;
            Debug.Log("Pressed the button " + InteractButtonName);
        }
    }
}
