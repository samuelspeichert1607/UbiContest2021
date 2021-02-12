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
    public float interactRadius = 3f;

    protected bool isInteractedWith = false;
    protected bool hasPlayerInRange = false;
    private bool previousPlayerRangeState = false;
    
    private bool playerHasEnteredRange = false;
    private bool playerHasLeftRange = false;


    private GameObject[] players;
    private GameObject inRangePlayer;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
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
            Debug.Log("Entered");
            
        }
        if (previousPlayerRangeState && !hasPlayerInRange)
        {
            playerHasEnteredRange = false;
            playerHasLeftRange = true;
            Debug.Log("Left");
        }
    }

    public virtual void OnInteract()
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
    
}
