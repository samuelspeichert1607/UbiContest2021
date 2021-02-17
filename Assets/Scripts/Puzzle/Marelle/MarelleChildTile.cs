using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleChildTile : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        
        transform.parent.GetComponent<MarelleTile>().CollisionDetected(collision.gameObject,this.gameObject);
    }

}
