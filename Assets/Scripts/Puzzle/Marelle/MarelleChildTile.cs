using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleChildTile : MonoBehaviour
{



    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        
        transform.parent.GetComponent<MarelleTile>().CollisionDetected(this.gameObject);
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    //}

}
