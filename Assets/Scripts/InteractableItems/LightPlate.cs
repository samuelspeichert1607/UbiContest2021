using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlate : PressurePlate
{
    [SerializeField] private LightPlateController controller;

    public override void CollisionEntered()
    {
        controller.CollisionDetected(this.gameObject);
        

    }


}
