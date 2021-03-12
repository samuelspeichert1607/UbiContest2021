using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateButton : PressurePlate
{
    private PressurePlateController controller;
    [SerializeField] private bool isTheRightOne = false;

    void Start()
    {
        controller = transform.parent.parent.parent.GetComponent<PressurePlateController>();//je sais.. mais sinon c'est tannant de glisser le script pour chaque plates...


    }

    public override void CollisionDetected()
    {

        goDown = true;
        if (controller.unlockedPlates)
        {

            
            if (isTheRightOne)
            {
                controller.won();

            }
            else
            {
                controller.penality();

            }
        }

    }


}
