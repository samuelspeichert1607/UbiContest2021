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
    // private AudioSource a;

    private ControllerManager controllerManager;

    private new void Start()
    {
        base.Start();
        // a = target.GetComponent<AudioSource>();
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
            target.MoveAtMaxSpeed(controllerManager.GetLeftAxisY(), controllerManager.GetLeftAxisX(), Time.deltaTime);
            // PlaySound();
            //Animation of the joystick would go here
        }

    }

    // public void PlaySound()
    // {
    //     if (!a.isPlaying)
    //     {
    //         a.Play();
    //     }
    // }

    public override void OnInteractStart()
    {
        IsInteractedWith = true;
        LockPlayerMovement();
        TextRenderer.ShowInfoText(ToEndInteractText);
    }

    private void AllowPlayerMovement()
    {
        CustomController playerController = GetInRangePlayer().GetComponent<CustomController>();
        if (!playerController.isMovementAllowed())
        {    
            playerController.allowMovement();
        }
    }

    private void LockPlayerMovement()
    {
        CustomController playerController = GetInRangePlayer().GetComponent<CustomController>();
        if (playerController.isMovementAllowed())
        {    
            playerController.disableMovement();
        }
    }
    
    public override void OnInteractEnd()
    {
        IsInteractedWith = false;
        AllowPlayerMovement();
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
        IsInteractedWith = false;
        AllowPlayerMovement();
        
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
