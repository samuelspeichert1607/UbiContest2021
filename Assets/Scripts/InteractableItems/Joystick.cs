using System;
using System.Collections;
using System.Collections.Generic;
using InteractableItems;
using UnityEngine;
using static InteractableItems.InteractableItem;
using static CustomController;

public class Joystick : InteractableItem
{
    
    [SerializeField] private CustomController target;

    private ControllerManager controllerManager;

    private new void Start()
    {
        base.Start();
        controllerManager = GetComponent<ControllerManager>();
    }
    
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
        else if (HasPlayerInRange)
        {
            OnPlayerInRange();
        }

        if (IsInteractedWith)
        {
            target.Move(controllerManager.GetLeftAxisY(), controllerManager.GetLeftAxisX(), Time.deltaTime);
            //Animation of the joystick would go here
        }

    }

    public override void OnInteractStart()
    {
        IsInteractedWith = true;
        TogglePlayerController();
        TextRenderer.ShowInfoText(ToEndInteractText);
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
        IsInteractedWith = false;
        TogglePlayerController();
        TextRenderer.ShowInfoText(ToStartInteractText);
    }

    public override void OnPlayerEnterRange()
    {
        FindTextRendererOfPlayerInRange();
        TextRenderer.ShowInfoText(ToStartInteractText);   
    }

    public override void OnPlayerExitRange()
    {
        TextRenderer.CloseInfoText();
    }

    public override void OnPlayerInRange()
    {
        if (controllerManager.GetButtonDown(interactButtonName))
        {
            if (IsInteractedWith)
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
