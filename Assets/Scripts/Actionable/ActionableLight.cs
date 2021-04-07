using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionableLight : Actionable
{
    [SerializeField] private GameObject lightObject;

    private void Start()
    {
        lightObject.SetActive(false);
    }

    public override void OnAction()
    {
        lightObject.SetActive(!lightObject.activeSelf);
    }
}
