using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    public float GetLeftAxisX();
    public float GetLeftAxisY();
    public float GetRightAxisX();
    public float GetRightAxisY();
    public float GetLTriggerAxis();
    public float GetRTriggerAxis();
    public bool GetButtonDown(string button);

}