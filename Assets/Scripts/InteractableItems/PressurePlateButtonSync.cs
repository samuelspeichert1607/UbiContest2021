using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateButtonSync : PressurePlate
{
    private bool isPressed = false;

    private Color initialColor;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        initialColor = _renderer.material.color;
    }

    public override void CollisionEntered()
    {
        if (!isLocked)
        {
            isPressed = true;
        }
    }

    public bool IsPressedAndUnlocked()
    {
        return isPressed && !isLocked;
    }

    public bool IsLocked()
    {
        return isLocked;
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public override void OnCollisionExit()
    {
        isPressed = false;
    }

    public void Reset()
    {
        isPressed = false;
        isLocked = false;
        SetColor(initialColor);
    }

    public void SetColor(Color color)
    {
        _renderer.material.SetColor("_Color", color);
    }
}
