using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class ParentTile : MonoBehaviourPun
{

    [SerializeField] protected AudioClip pressedSound;
    [SerializeField] protected AudioSource audioSource;
    public abstract void CollisionDetected(GameObject sourceTile);

    public void CollisionExited()
    {
        audioSource.PlayOneShot(pressedSound);
    }


    public void ChangeColor(Color color)
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<TileGoUpDown>().tileRenderer.material.SetColor("_Color", color);
        }

    }
    
}
