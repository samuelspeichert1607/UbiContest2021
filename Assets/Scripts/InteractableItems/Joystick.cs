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
    private TextRenderer textRenderer;

    private new void Start()
    {
        base.Start();
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
            target.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Time.deltaTime);
            //Animation of the joystick would go here
        }

    }

    public override void OnInteractStart()
    {
        IsInteractedWith = true;
        TogglePlayerController();
        textRenderer.ShowInfoText(ToEndInteractText);
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
        textRenderer.ShowInfoText(ToStartInteractText);
    }

    public override void OnPlayerEnterRange()
    {
        FindTextRendererOfPlayerInRange();
        textRenderer.ShowInfoText(ToStartInteractText);   
    }

    public override void OnPlayerExitRange()
    {
        textRenderer.CloseInfoText();
    }

    public override void OnPlayerInRange()
    {
        if (Input.GetButtonDown(interactButtonName))
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
