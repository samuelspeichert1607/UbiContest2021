using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlateActivateObject : PressurePlate
{
    [SerializeField] private Actionable[] actionableObject;
    private Material mat;

     void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    public override void CollisionEntered()
    {
        if (mat.color!=Color.green)
        {
            mat.SetColor("_Color", Color.green);
            ToggleActionable();        }

    }

private void ToggleActionable()
    {
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }

    }


}
