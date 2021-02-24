using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ParentTile : MonoBehaviourPun//, IPunObservable
{

    private GameObject tileExited = null;
    public abstract void CollisionDetected(GameObject sourceTile);

    public void ChangeColor(Color color)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", color);
        }

    }
    public void CollisionExited(GameObject sourceTile)
    {
        if (transform.parent.GetComponent<MarelleWon>().unlockCollision)
        {
            sourceTile.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            if (tileExited == null)
            {
                tileExited = sourceTile;
            }
            else if (sourceTile != tileExited)
            {
                tileExited = null;
                ChangeColor(Color.white);

            }
        }
    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    //if (stream.IsWriting)
    //    //{
    //    //    stream.SendNext(tileMaterial1);
    //    //    stream.SendNext(tileMaterial2);
    //    //}
    //    //else if (stream.IsReading)
    //    //{
    //    //    tileMaterial1 = (Material)stream.ReceiveNext();
    //    //    tileMaterial2 = (Material)stream.ReceiveNext();
    //    //}
    //}

}
