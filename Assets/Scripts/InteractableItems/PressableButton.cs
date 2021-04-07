using System.Collections;
using System.Collections.Generic;
using System.Timers;
using InteractableItems;
using UnityEngine;

public class PressableButton : InteractableItem
{
    // Start is called before the first frame update
    [SerializeField] private float pressingTime;
    // private ControllerManager controllerManager;

    private new void Start()
    {
        base.Start();
        // controllerManager = GetComponent<ControllerManager>();
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
        Invoke("OnInteractEnd", pressingTime);
        TextRenderer.CloseInfoText();
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
    
    public bool IsPressed()
    {
        return IsInteractedWith;
    }

    public override void OnPlayerInRange()
    {
        if (ControllerManager.GetButtonDown(interactButtonName) && !IsInteractedWith)
        {
            OnInteractStart();
        }
    }
}
