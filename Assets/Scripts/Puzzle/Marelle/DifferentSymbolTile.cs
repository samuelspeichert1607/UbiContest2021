using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifferentSymbolTile : ParentTile
{
    private MarelleController marelleController;
    private void Start()
    {
         marelleController = transform.parent.GetComponent<MarelleController>();
    }
    public override void CollisionDetected(GameObject sourceTile)
    {
        
        
        if (marelleController.hasCollisionUnlocked && sourceTile.transform.GetComponent<TileGoUpDown>().tileRenderer.material.color!=Color.green)
        {
            marelleController.gameLost();
        }
    }
}
