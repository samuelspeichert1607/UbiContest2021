using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MarelleTile : MonoBehaviourPun, IPunObservable
{

    
    [SerializeField] private bool firstTile = false;


    Material tileMaterial1;
    Material tileMaterial2;

    private float timerTime;
    private float timer =0;
    private bool timerEnable = false;

    private GameObject tileEntered=null;
    //private GameObject playerExited = null;

    

    void Start()
    {
        timerTime = transform.parent.GetComponent<MarelleWon>().timerTime;
        tileMaterial1 = transform.GetChild(0).GetComponent<Renderer>().material;
        tileMaterial2 = transform.GetChild(1).GetComponent<Renderer>().material;
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
                timer = 0;
                tileEntered = null;
            }
        }



    }


    public void CollisionDetected(GameObject sourceTile) //quand on échoue la premier tuile marche pas si le meme joueur saute en premier 2 fois
    {

        bool isResolve = transform.parent.GetComponent<MarelleWon>().isResolve;


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

                }
                else
                {

                    transform.parent.GetComponent<MarelleWon>().isResolve = false;

                }

            }

        }
        //else if (!firstTile)
        //{
        //    ChangeColor(Color.red);
        //}



    }

    //public void CollisionExited(GameObject player)
    //{
    //    if (playerExited == null)
    //    {
    //        playerExited = player;
    //    }
    //    else if (playerExited!= player)
    //    {
    //        playerExited = null;
    //        ChangeColor(Color.white);
    //    }


    //}

    private void ChangeColor(Color color)
    {
        tileMaterial1.SetColor("_Color", color);
        tileMaterial2.SetColor("_Color", color);

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tileMaterial1);
            stream.SendNext(tileMaterial2);
        }
        else if (stream.IsReading)
        {
            tileMaterial1 = (Material)stream.ReceiveNext();
            tileMaterial2 = (Material)stream.ReceiveNext();
        }
    }

}
