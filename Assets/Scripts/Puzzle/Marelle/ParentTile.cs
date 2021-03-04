using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ParentTile : MonoBehaviourPun
{

    public abstract void CollisionDetected(GameObject sourceTile);


    public void ChangeColor(Color color)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", color);
        }

    }


}
