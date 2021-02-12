using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public string Name;
    public string InteractPreButtonText;
    public string InteractPostButtonText;
    public string InteractButton;
    public float interactRadius = 3f;

    protected bool isInteractedWith = false;
    protected bool hasPlayerInRange = false;


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
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance <= interactRadius)
            {
                hasPlayerInRange = true;
                inRangePlayer = player;
                return;
            }
        }
        hasPlayerInRange = false;
    }
    
    public virtual void OnInteract()
    {
        
    }
    
    public virtual void OnPlayerInRange()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
    
}
