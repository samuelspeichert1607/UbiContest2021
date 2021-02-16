using System;
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
        if (hasPlayerEnteredRange())
        {
            OnPlayerEnterRange();
        }
        else if (hasPlayerLeftRange())
        {
            OnPlayerExitRange();
        }
        else if (hasPlayerInRange)
        {
            OnPlayerInRange();
        }

        if (isInteractedWith)
        {
            target.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Time.deltaTime);
            //Animation of the joystick would go here
        }

    }

    public override void OnInteractStart()
    {
        isInteractedWith = true;
        TogglePlayerController();
        textRenderer.ShowInfoText(toEndInteractText);
    }

    private void TogglePlayerController()
    {
        try
        {
            CustomController playerController = GetInRangePlayer().GetComponent<CustomController>();
            playerController.ToggleMovement();
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Error" + e.Message);
        }
    }

    public override void OnInteractEnd()
    {
        isInteractedWith = false;
        TogglePlayerController();
        textRenderer.ShowInfoText(toStartInteractText);
    }

    public override void OnPlayerEnterRange()
    {
        textRenderer.ShowInfoText(toStartInteractText);   
    }

    public override void OnPlayerExitRange()
    {
        textRenderer.CloseInfoText();
    }

    public override void OnPlayerInRange()
    {
        if (Input.GetButtonDown(InteractButtonName))
        {
            if (isInteractedWith)
            {
                OnInteractEnd();   
            }
            else
            {
                OnInteractStart();
            }
        }
    }
}
