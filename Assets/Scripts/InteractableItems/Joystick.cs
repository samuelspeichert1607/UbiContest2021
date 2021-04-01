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

    private Animator _leverAnimator;
    private ControllerManager _controllerManager;
    private int _leftAxisYUnitDirection = 1;
    private int _leftAxisXUnitDirection = 1;
    private bool _swappedMotionAxis = false;

    private new void Start()
    {
        base.Start();
        _controllerManager = GetComponent<ControllerManager>();
        _leverAnimator = GetComponentInChildren<Animator>();
        SetUnitDirectionRelativeToTarget();
        // _leverAnimator.SetFloat("inputV", 0, 0.1f, Time.deltaTime);
        // _leverAnimator.SetFloat("inputH", 0, 0.1f, Time.deltaTime);
    }

    private void SetUnitDirectionRelativeToTarget()
    {
        int targetYAngle = (int) target.transform.eulerAngles.y;
        int selfYAngle = (int) transform.eulerAngles.y;
        
        if (selfYAngle == (targetYAngle + 90) % 360)
        {
            _swappedMotionAxis = true;
            _leftAxisYUnitDirection = -1;
        }
        else if (selfYAngle == (targetYAngle + 180) % 360)
        {
            
            _leftAxisYUnitDirection = -1;
            _leftAxisXUnitDirection = -1;
        }
        else if (selfYAngle == (targetYAngle + 270) % 360)
        {
            _swappedMotionAxis = true;
            _leftAxisXUnitDirection = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_leverAnimator.GetFloat("inputH"));
        Debug.Log(_leverAnimator.GetFloat("inputV"));
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
            MoveTarget();
        }

    }

    private void MoveTarget()
    {
        if (_swappedMotionAxis)
        {
            target.MoveAtMaxSpeed( _leftAxisYUnitDirection * _controllerManager.GetLeftAxisX(),
                _leftAxisXUnitDirection * _controllerManager.GetLeftAxisY(), Time.deltaTime);
        }
        else
        {
            target.MoveAtMaxSpeed( _leftAxisYUnitDirection * _controllerManager.GetLeftAxisY(),
                _leftAxisXUnitDirection * _controllerManager.GetLeftAxisX(), Time.deltaTime);
        }
        //Animation of the joystick would go here
        AnimateLever(_controllerManager.GetLeftAxisX(), _controllerManager.GetLeftAxisY());
    }

    private void AnimateLever(float horizontalMotion, float verticalMotion)
    {
        if (verticalMotion < -0.25f)
        {
            //Down
            _leverAnimator.SetFloat("inputV", -1, 0.1f, Time.deltaTime);
        }
        else if (verticalMotion > 0.25f)
        {
            //Up
            _leverAnimator.SetFloat("inputV", 1, 0.1f, Time.deltaTime);
        }
        else if (horizontalMotion < -0.25f)
        {
            //Left
            _leverAnimator.SetFloat("inputH", -1, 0.1f, Time.deltaTime);
        }
        else if (horizontalMotion > 0.25f)
        {
            //Right
            _leverAnimator.SetFloat("inputH", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            _leverAnimator.SetFloat("inputV", 0, 0.1f, Time.deltaTime);
            _leverAnimator.SetFloat("inputH", 0, 0.1f, Time.deltaTime);
        }
    }

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
        _leverAnimator.SetFloat("inputV", 0, 0.1f, Time.deltaTime);
        _leverAnimator.SetFloat("inputH", 0, 0.1f, Time.deltaTime);
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
        if (_controllerManager.GetButtonDown(interactButtonName))
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
