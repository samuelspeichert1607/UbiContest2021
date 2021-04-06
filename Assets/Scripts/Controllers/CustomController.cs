using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomController : MonoBehaviour
{

    protected bool canMove = true;
    protected bool isInCriticalMotion = false;
    public virtual void Move(Vector3 speed, float timeElapsed)
    {
        
    }
    
    public virtual void MoveAtMaxSpeed(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        
    }

    public void ToggleMovement()
    {
        if (canMove)
        {
            disableMovement();
        }
        else
        {
            allowMovement();
        }
    }

    public void disableMovement()
    {
        canMove = false;
    }

    public void allowMovement()
    {
        canMove = true;
    }

    public bool isMovementAllowed()
    {
        return canMove;
    }
    
    /// <summary>
    ///  If the controller is currently in a motion that should not be disturbed 
    /// </summary>
    /// <returns></returns>
    public bool IsInCriticalMotion()
    {
        return isInCriticalMotion;
    }

    public virtual void PlayDeathAnimation()
    {
        
    }
}
