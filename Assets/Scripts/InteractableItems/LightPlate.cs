using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlate : PressurePlate
{
    [SerializeField] private LightPlateController controller;
    private void Start()
    {
        //controller = transform.parent.parent.parent.GetComponent<LightPlateController>();
    }
    public override void CollisionEntered()
    {
        controller.CollisionDetected(this.gameObject);
        

    }


}
