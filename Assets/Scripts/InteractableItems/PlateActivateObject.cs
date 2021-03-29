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
        if (mat.color!=Color.green)
        {
            mat.SetColor("_Color", Color.green);
            ToggleActionable();
            Invoke("CloseActinable", waitTime);
        }

    }

    private void CloseActinable()
    {
        mat.SetColor("_Color", originalColor);
        ToggleActionable();

    }
private void ToggleActionable()
    {
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }

    }


}
