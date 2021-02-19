using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PressableButton : InteractableItem
{
    // Start is called before the first frame update
    
    [SerializeField] private TextRenderer textRenderer;
    [SerializeField] private float pressingTime;

    private void Start()
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
        else if (hasPlayerInRange)
        {
            OnPlayerInRange();
        }
    }
    
    public override void OnInteractStart()
    {
        isInteractedWith = true;
        Invoke("OnInteractEnd", pressingTime);
        textRenderer.CloseInfoText();
    }
    

    public override void OnInteractEnd()
    {
        isInteractedWith = false;
        textRenderer.ShowInfoText(toStartInteractText);
    }

    public override void OnPlayerEnterRange()
    {
        FindTextRendererOfPlayerInRange();
        textRenderer.ShowInfoText(toStartInteractText);   
    }

    public override void OnPlayerExitRange()
    {
        textRenderer.CloseInfoText();
    }
    
    public bool IsPressed()
    {
        return isInteractedWith;
    }

    public override void OnPlayerInRange()
    {
        if (Input.GetButtonDown(InteractButtonName) && !isInteractedWith)
        {
            OnInteractStart();
        }
    }

    private void FindTextRendererOfPlayerInRange()
    {
        textRenderer = GetInRangePlayer().GetComponentInChildren<TextRenderer>();
    }
}
