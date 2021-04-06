using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SameSymbolTile : ParentTile
{

    [SerializeField] private bool lastTile = false;
    [SerializeField] private ParentTile[] otherTiles;


    private float timerTime;
    private float timer = 0;
    private bool timerEnable = false;

    private GameObject tileEntered = null;

    private MarelleController marelleController;

    private PhotonView _photonView ;

    void Start()
    {
        _photonView = GetComponent<PhotonView>();
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


    }

    public override void CollisionDetected(GameObject sourceTile) 
    {
        Debug.Log(sourceTile);
        audioSource.PlayOneShot(pressedSound);
        Material sourceMat = sourceTile.transform.GetComponent<TileGoUpDown>().tileRenderer.material;
        if (!(sourceMat.color==Color.green))
        {


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

                }

            }
                //bombe
            //else if (!firstTile)
            //{
            //    marelleController.gameLost();
            //}

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
