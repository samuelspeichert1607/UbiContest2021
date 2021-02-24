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


    void Start()
    {
        timerTime = transform.parent.GetComponent<MarelleWon>().timerTime;

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

                ChangeColor(Color.red);
                timerEnable = false;
                timer = 0;
                tileEntered = null;
                transform.parent.GetComponent<MarelleWon>().isResolve = false;
            }
        }


    }

    public override void CollisionDetected(GameObject sourceTile) //quand on �choue la premier tuile marche pas si le meme joueur saute en premier 2 fois
    {
       
        if (transform.parent.GetComponent<MarelleWon>().unlockCollision)
        {
            bool isResolve = transform.parent.GetComponent<MarelleWon>().isResolve;
            testBool = true;

            if ((!firstTile && isResolve) || (firstTile && !isResolve))
            {
                if (tileEntered == null)
                {
                    tileEntered = sourceTile;
                    timerEnable = true;
                    timer = timerTime;
                    sourceTile.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                }

                else if (tileEntered != sourceTile)
                {
                    timerEnable = false;
                    tileEntered = null;
                    if (timer > 0)
                    {

                        ChangeColor(Color.green);
                        transform.parent.GetComponent<MarelleWon>().isResolve = true;
                        if (lastTile)
                        {
                            transform.parent.GetComponent<MarelleWon>().gameWon();
                        }

                    }

                }

            }
            else if (!firstTile)
            {
                ChangeColor(Color.red);
            }
            else
            {
                ChangeColor(Color.green);
            }

        }
    }


}
