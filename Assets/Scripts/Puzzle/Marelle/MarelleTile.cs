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

    private GameObject playerEntered=null;

    

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
                ChangeColour(Color.red);
                timer = 0;
            }
        }



    }


    public void CollisionDetected(GameObject collision,GameObject sourceTile)
    {
        
        bool isResolve = transform.parent.GetComponent<MarelleWon>().isResolve;
        //

        if ((!firstTile&& isResolve) || (firstTile && !isResolve))
        {
            //playerEntered contient le premier joueur à entrer en collision avec la tuile
            if (playerEntered == null || firstTile)//hmm
            {
                playerEntered = collision;
                timerEnable = true;
                timer = timerTime;
                sourceTile.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                
            }
            //si un deuxième joueur entre en collision on commence le timer
            else if (playerEntered != collision)
            {
                timerEnable = false;
                playerEntered = null;
                if (timer > 0)
                {

                    ChangeColour(Color.green);
                    transform.parent.GetComponent<MarelleWon>().isResolve = true;
                    
                }
                else
                {

                    transform.parent.GetComponent<MarelleWon>().isResolve = false;
                    
                }

            }
        }



    }

    private void ChangeColour(Color color)
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
