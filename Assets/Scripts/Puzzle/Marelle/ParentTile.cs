using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ParentTile : MonoBehaviourPun
{

    public abstract void CollisionDetected(GameObject sourceTile);


    public void ChangeColor(Color color)
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<TileGoUpDown>().tileRenderer.material.SetColor("_Color", color);
        }

    }


}
