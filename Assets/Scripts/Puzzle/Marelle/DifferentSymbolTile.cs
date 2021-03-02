using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentSymbolTile : ParentTile
{
    public override void CollisionDetected(GameObject sourceTile)
    {
        if (transform.parent.GetComponent<MarelleWon>().unlockCollision)
        {
            //ChangeColor(Color.red);
            transform.parent.GetComponent<MarelleWon>().gameLost();
            transform.parent.GetComponent<MarelleWon>().isResolve = false;
        }
    }
}
