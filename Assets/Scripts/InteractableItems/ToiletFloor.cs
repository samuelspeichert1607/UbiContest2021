using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletFloor : MonoBehaviour
{
    [SerializeField] RobotVoiceController robot;
    [SerializeField] Actionable door;
    private bool UnLock = true;
    // Start is called before the first frame update
    public void OnFloor()
    {
        if (UnLock)
        {
            UnLock = false;
            robot.PlayNoPooping();
            door.OnAction();
        }

    }
}
