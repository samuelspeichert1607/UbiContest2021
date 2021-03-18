using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Platform : ParentTile
{
    public Renderer tileRenderer;

    private Animation anim;
    private BoxCollider plateformeColider;
    private Color defaultColor;
    private bool isPressed = false;
    private bool isUnlocked = true;
    private float _pressTime = 1.0f;


    private void Start()
    {
        plateformeColider = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animation>();
        defaultColor = tileRenderer.material.color;
    }

    public void SetPressTime(float pressTime)
    {
        _pressTime = pressTime;
    }

    public override void CollisionExited()
    {
        isPressed = false;
    }

    public override void CollisionDetected(GameObject sourceTile)
    {
        if (isUnlocked)
        {
            isPressed = true;
        }
    }

    private void Unpress()
    {
        isPressed = false;
    } 


    public void PlayAnimation()
    {
        anim.Play();
        plateformeColider.enabled = false;
        Invoke("ResetPlatform", 1);
        
    }
    public void SetColor(Color color)
    {
        tileRenderer.material.SetColor("_Color", color);
    }

    public void Reset()
    {
        plateformeColider.enabled = true;
        SetColor(defaultColor);
    }
    
    public void Lock()
    {
        isUnlocked = false;
    }

    public void Unlock()
    {
        isUnlocked = true;
    }

    public bool IsPressedAndUnlocked()
    {
        return IsPressed() && IsUnlocked();
    }

    public bool IsPressed()
    {
        return isPressed;
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
