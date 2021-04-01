using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SameSymbolTile : ParentTile
{

    [SerializeField] private bool lastTile = false;
    [SerializeField] private ParentTile[] otherTiles;

    private PhotonView _photonView ;
    private float timerTime;
    private float timer = 0;
    private bool timerEnable = false;

    private GameObject tileEntered = null;

    bool testBool = true;
    private MarelleController marelleController;

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
        //cheat code
        if (Input.GetButtonDown("Fire2") && testBool)
        {
            CollisionDetected(transform.GetChild(0).gameObject);
            testBool = false;
        }

    }

    public override void CollisionDetected(GameObject sourceTile) 
    {
        audioSource.PlayOneShot(pressedSound);
        Material sourceMat = sourceTile.transform.GetComponent<TileGoUpDown>().tileRenderer.material;
        if (!(sourceMat.color==Color.green))
        {
            testBool = true;

                if (tileEntered == null)//^tileEntered ==sourceTile
            {
                    tileEntered = sourceTile;
                _photonView.RPC(nameof(EnableTimer), RpcTarget.All);
                //timerEnable = true;

                timer = timerTime;
                    ChangeColor(Color.yellow);

            }

            else if (tileEntered != sourceTile)
            {
                //timerEnable = false;
                _photonView.RPC(nameof(DisableTimer), RpcTarget.All);
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
    [PunRPC]
    private void EnableTimer()
    {
        timerEnable = true;
    }
    [PunRPC]
    private void DisableTimer()
    {
        timerEnable = false;
    }
}
