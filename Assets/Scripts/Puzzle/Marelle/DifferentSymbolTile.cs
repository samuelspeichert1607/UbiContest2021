using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentSymbolTile : ParentTile
{
    public override void CollisionDetected(GameObject sourceTile)
    {
        MarelleController marelleController = transform.parent.GetComponent<MarelleController>();
        if (marelleController.hasCollisionUnlocked)
        {
            marelleController.gameLost();
            marelleController.isResolve = false;
        }
    }
}
