using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlateActivateObject : PressurePlate
{
    [SerializeField] private float waitTime;
    [SerializeField] private Actionable[] actionableObject;
    private Material mat;
    private Color originalColor;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        originalColor = mat.color;
    }
    public override void CollisionEntered()
    {
        if (mat.color==originalColor)
        {
            mat.SetColor("_Color", Color.green);
            ToggleActionable();
            Invoke(nameof(CloseActionable), waitTime);
        }

    }

    private void CloseActionable()
    {
        mat.SetColor("_Color", Color.red);
        ToggleActionable();
        Invoke(nameof(ResetPlate),2);

    }
    private void ToggleActionable()
    {
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }

    }

    private void ResetPlate()
    {
        mat.SetColor("_Color", originalColor);
    }

}