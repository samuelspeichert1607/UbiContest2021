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
        
        
        if (marelleController.hasCollisionUnlocked && IsSourceTileNotGreen(sourceTile))
        {
            marelleController.gameLost();
        }
    }

    private bool IsSourceTileNotGreen(GameObject sourceTile)
    {
        return sourceTile.transform.GetComponent<TileGoUpDown>().tileRenderer.material.color != Color.green;
    }

    public override void CollisionExited()
    {
    }
}
