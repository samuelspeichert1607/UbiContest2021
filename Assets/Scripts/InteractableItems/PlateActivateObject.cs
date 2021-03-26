using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlateActivateObject : PressurePlate
{
    [SerializeField] private float waitTime;
    [SerializeField] private Actionable[] actionableObject;
    private Material mat;
    private Color startColor;
    private bool IsUnlock = true;
     void Start()
    {
        mat = GetComponent<Renderer>().material;
        startColor = mat.color;
    }
    public override void CollisionEntered()
    {
        if (IsUnlock)
        {
            //faire joueur un message qui dit d<attendre l<autre joueur
            mat.SetColor("_Color", Color.green);
            ToggleActionable();
            Invoke("CloseActinable", waitTime);
            IsUnlock = false;
        }

    }

    private void CloseActinable()
    {
        mat.SetColor("_Color", Color.red);
        ToggleActionable();
        Invoke("UnlockActionable", waitTime);

    }
private void ToggleActionable()
    {
        foreach (Actionable a in actionableObject)
        {
            
            a.OnAction();
        }

    }



    private void UnlockActionable()
    {
        IsUnlock = true;
        mat.SetColor("_Color", startColor);
    }
}
