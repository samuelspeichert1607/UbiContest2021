using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateButtonSync : PressurePlate
{
    private bool isUnlocked = true;

    void Start()
    {
    
    }

    public override void CollisionEntered()
    {

        if (isUnlocked)
        {
            
        }

    }
}
