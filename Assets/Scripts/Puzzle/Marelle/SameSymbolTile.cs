using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SameSymbolTile : ParentTile
{


    [SerializeField] private bool firstTile = false;
    [SerializeField] private bool lastTile = false;


    private float timerTime;
    private float timer = 0;
    private bool timerEnable = false;

    private GameObject tileEntered = null;

    bool testBool = false;
    private MarelleController marelleController;

    void Start()
    {
        marelleController = transform.parent.GetComponent<MarelleController>();
        timerTime = marelleController.timerTime;
    }


    void Update()
    {
        if (timerEnable)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                marelleController.gameLost();
                timerEnable = false;
                timer = 0;
                tileEntered = null;
                marelleController.isResolve = false;
            }
        }
        if (Input.GetButtonDown("Fire2") && testBool)
        {
            CollisionDetected(transform.GetChild(1).gameObject);
            testBool = false;


        }

    }

    public override void CollisionDetected(GameObject sourceTile) 
    {
        Material sourceMat = sourceTile.GetComponent<Renderer>().material;
        if ((marelleController.hasCollisionUnlocked || firstTile) && !(sourceMat.color==Color.green))
        {
            testBool = true;

            //if ((!firstTile && isResolve) || (firstTile && !isResolve)) //je pense qu<il sert 'a rien maintenant mais j,ai peur d<y toucher
            //{
                if (tileEntered == null)
                {
                    tileEntered = sourceTile;
                    timerEnable = true;
                    timer = timerTime;
                    sourceMat.SetColor("_Color", Color.yellow);
                    
                }

                else if (tileEntered != sourceTile)
                {
                    timerEnable = false;
                    tileEntered = null;
                    if (timer > 0)
                    {

                        ChangeColor(Color.green);
                        marelleController.isResolve = true;
                        if (lastTile)
                        {
                        marelleController.gameWon();
                        }
                        else if (firstTile)
                        {
                        marelleController.resetMarelle();
                        }

                    }

                }

           // }
            else if (!firstTile)
            {

                marelleController.gameLost();
            }
            else
            {
                ChangeColor(Color.green);
            }

        }


    }


}
