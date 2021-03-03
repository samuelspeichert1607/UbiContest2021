using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentSymbolTile : ParentTile
{
    public override void CollisionDetected(GameObject sourceTile)
    {
        MarelleWon marelleWon = transform.parent.GetComponent<MarelleWon>();
        if (marelleWon.unlockCollision)
        {
            marelleWon.gameLost();
            marelleWon.isResolve = false;
        }
    }
}
