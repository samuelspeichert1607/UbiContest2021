using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SameSymbolTile : ParentTile
{


    [SerializeField] private bool firstTile = false;
    [SerializeField] private bool lastTile = false;
    [SerializeField] private ParentTile[] otherTiles;


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
            }
        }
        //cheat code
        if (Input.GetButtonDown("Fire2") && testBool)
        {
            CollisionDetected(transform.GetChild(0).gameObject);
            testBool = false;
        }

    }

    public override void CollisionDetected(GameObject sourceTile) 
    {
        Material sourceMat = sourceTile.transform.GetComponent<TileGoUpDown>().tileRenderer.material;
        if ((marelleController.hasCollisionUnlocked || firstTile) && !(sourceMat.color==Color.green))
        {
            testBool = true;

                if (tileEntered == null)//^tileEntered ==sourceTile
            {
                    tileEntered = sourceTile;
                    timerEnable = true;
                    timer = timerTime;
                    ChangeColor(Color.yellow);

            }

                else if (tileEntered != sourceTile)
                {
                timerEnable = false;
                    tileEntered = null;
                    if (timer > 0)
                    {

                        ChangeColorToGreen();
                    if (lastTile)
                        {
                            marelleController.gameWon();
                        }
                    else if (firstTile)
                    {
                        marelleController.hasCollisionUnlocked = true;
                    }

                    }

                }

            else if (!firstTile)
            {
                marelleController.gameLost();
            }

        }


    }

    private void ChangeColorToGreen()
    {
        ChangeColor(Color.green);
        foreach(ParentTile tile in otherTiles)
        {
            tile.ChangeColor(Color.green);
        }
    }
}
