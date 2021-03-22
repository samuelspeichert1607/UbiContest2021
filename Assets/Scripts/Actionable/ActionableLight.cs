using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableLight : Actionable
{
    [SerializeField] private Light light;

    private void Start()
    {
        light.enabled = false;
    }

    public override void OnAction()
    {
        
        light.enabled = !light.enabled;
    }
}
