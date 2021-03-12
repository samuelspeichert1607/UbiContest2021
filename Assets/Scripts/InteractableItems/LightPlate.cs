using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlate : PressurePlate
{
    private LightPlateController controller;
    private void Start()
    {
        controller = transform.parent.parent.parent.GetComponent<LightPlateController>();
    }
    public override void CollisionEntered()
    {
        controller.CollisionDetected(this.gameObject);


    }
    public void ChangeColor(Color color)
    {
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

}
