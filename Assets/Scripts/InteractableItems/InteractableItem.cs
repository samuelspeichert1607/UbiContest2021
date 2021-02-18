using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableItem : MonoBehaviour
{
    public string Name;
    public string InteractPreButtonText;
    public string InteractPostButtonText;
    public string InteractButtonName = "B";
    public string InteractionStopPostButtonText;
    public float interactRadius = 3f;

    protected bool isInteractedWith = false;
    protected bool hasPlayerInRange = false;
    protected string toStartInteractText;
    protected string toEndInteractText;
    
    private GameObject inRangePlayer;
    
    private bool previousPlayerRangeState = false;
    private bool playerHasEnteredRange = false;
    private bool playerHasLeftRange = false;

    protected IController UserController;
    private ControllerPicker controllerPicker;


    private GameObject[] players;

    protected void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        toStartInteractText = String.Join(" ", InteractPreButtonText, InteractButtonName, InteractPostButtonText);
        toEndInteractText = String.Join(" ", InteractPreButtonText, InteractButtonName, InteractionStopPostButtonText);
        controllerPicker = new ControllerPicker();
    }

    private void Update()
    {

    }

    protected void CheckIfAPlayerIsInRange()
    {
        previousPlayerRangeState = hasPlayerInRange;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= interactRadius)
            {
                hasPlayerInRange = true;
                inRangePlayer = player;
                UpdatePlayerRangeState();
                return;
            }
        }
        hasPlayerInRange = false;
        UpdatePlayerRangeState();
    }
    
    

    private void UpdatePlayerRangeState()
    {
        if (previousPlayerRangeState == hasPlayerInRange)
        {
            playerHasEnteredRange = false;
            playerHasLeftRange = false;
        }

        if (!previousPlayerRangeState && hasPlayerInRange)
        {
            playerHasEnteredRange = true;
            playerHasLeftRange = false;
        }
        if (previousPlayerRangeState && !hasPlayerInRange)
        {
            playerHasEnteredRange = false;
            playerHasLeftRange = true;
        }
    }

    protected GameObject GetInRangePlayer()
    {
        // if (inRangePlayer == null)
        // {
        //     throw new NullReferenceException();
        // }
        return inRangePlayer;
    }


    public virtual void OnInteractStart()
    {
        
    }

    public virtual void OnInteractEnd()
    {
        
    }
    
    public virtual void OnPlayerEnterRange()
    {

    }

    public virtual void OnPlayerInRange()
    {
        
    }

    public virtual void OnPlayerExitRange()
    {
        
    }
    
    protected bool hasPlayerEnteredRange()
    {
        return playerHasEnteredRange;
    }

    protected bool hasPlayerLeftRange()
    {
        return playerHasLeftRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
    
    public void PickController()
    {
        string currentController = Input.GetJoystickNames()[0];
        if (controllerPicker.IsDifferentController(currentController))
        {
            UserController = controllerPicker.PickController(currentController);
        }
    }
    
}
