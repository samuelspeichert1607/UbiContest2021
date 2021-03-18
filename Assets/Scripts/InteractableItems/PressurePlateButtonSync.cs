using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateButtonSync : PressurePlate
{
    private bool isUnlocked = true;
    private Renderer _renderer;
    private Color defaultColor;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        defaultColor = _renderer.material.color;
    }


    public override void CollisionEntered()
    {
    }
    
    public bool IsPressedAndUnlocked()
    {
        return IsPressed() && IsUnlocked();
    }

    public void Reset()
    {
        SetColor(defaultColor);
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }

    public void Lock()
    {
        isUnlocked = false;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void SetColor(Color color)
    {
        _renderer.material.SetColor("_Color", color);
    }
}
