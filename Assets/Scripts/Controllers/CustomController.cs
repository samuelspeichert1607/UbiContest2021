using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomController : MonoBehaviour
{

    protected bool canMove = true;
    public virtual void Move(float verticalMotion, float horizontalMotion, float timeElapsed)
    {
        
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
