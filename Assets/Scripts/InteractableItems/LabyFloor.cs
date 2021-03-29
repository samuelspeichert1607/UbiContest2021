using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabyFloor : MonoBehaviour
{
    [SerializeField] private RobotVoiceController robot;
    private bool canEnter = true;
    public void FellOnFloor()
    {
        
        if (canEnter)
        {
            canEnter = false;
            robot.PlayTaskFailed();
        }
    }
}
