using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightFlickering : MonoBehaviour
{
    [SerializeField] private Actionable[] doorsToClose;
    [SerializeField] private GameObject lightsToFlicker;
    [SerializeField] private  float modifyTimer;
    [SerializeField] private  float lightOnTimer;
    [SerializeField] private  float lightOffTimer;
    [SerializeField] private  float flickeringDuration;
    private bool canFlicker = true;
    private bool lightOn = true;
    private bool Unlock = true;


    public void StartFlicker()
    {
        if (Unlock)
        {
            foreach (Actionable door in doorsToClose)
            {
                door.OnAction();
            }
            Unlock = false;
            ToggleLight();
            Invoke("StopFlicker",flickeringDuration);
            
        }

    }

    private void StopFlicker()
    {
        canFlicker = false;
    }

    private void ToggleLight()
    {
        lightOnTimer -= modifyTimer;
        lightOffTimer += modifyTimer;
        lightOn = !lightOn;

        lightsToFlicker.SetActive(lightOn);


        if (canFlicker||lightOn)
        {
            if (lightOn)
            {
                Invoke("ToggleLight",lightOnTimer);
            }
            else
            {
                Invoke("ToggleLight",lightOffTimer);
            }
            
        }
    }
}
