using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomController : MonoBehaviour
{

    protected bool canMove = true;
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
}
