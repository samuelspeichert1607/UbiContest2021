using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    float GetAxis(string axis);
    float GetLeftAxisX();
    float GetLeftAxisY();
    float GetRightAxisX();
    float GetRightAxisY();
    float GetLTriggerAxis();
    float GetRTriggerAxis();
    bool GetButtonDown(string button);

    bool GetButton(string button);
}
