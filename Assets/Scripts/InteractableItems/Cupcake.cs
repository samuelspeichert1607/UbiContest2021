using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractableItems;

public class Cupcake : InteractableItem
{
    // Start is called before the first frame update
    private ControllerManager _controllerManager;
    

    private new void Start()
    {
        base.Start();
        _controllerManager = GetComponent<ControllerManager>();
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
    }
    
    public override void OnInteractStart()
    {
        IsInteractedWith = true;
        TextRenderer.CloseInfoText();
        
        GetInRangePlayer().GetComponent<CustomController>().disableMovement();
        GetInRangePlayer().GetComponentInChildren<StatusHUD>().CallGameSucceededAfterDefaultDelay();
    }
    
    public override void OnInteractEnd()
    {
        IsInteractedWith = false;
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
        if (_controllerManager.GetButtonDown(interactButtonName) && !IsInteractedWith)
        {
            OnInteractStart();
        }
    }
}
