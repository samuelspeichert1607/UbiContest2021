using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleController : MonoBehaviour
{
    public bool isResolve =false;
    public float timerTime;
    public bool hasCollisionUnlocked =true;

    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        hasCollisionUnlocked = true;
    }
    public void gameWon()
    {
        hasCollisionUnlocked = false;
        transform.GetChild(0).GetComponent<MovingWall>().CanMove = true;
        for (int i = 1; i < transform.childCount - 2; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }


    }

    public void gameLost()
    {
        hasCollisionUnlocked = false;
        for (int i = 1; i < transform.childCount - 2; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Transform tile = transform.GetChild(i).GetChild(j);
                if (i == 1)
                {
                    tile.GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }
                else
                {
                    tile.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    tile.GetComponent<TileGoUpDown>().CanGoDown = true;
                }
                
            }
        }
    }

    public void resetMarelle()
    {
        hasCollisionUnlocked = true;
        for (int i = 2; i < transform.childCount - 2; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Transform tile = transform.GetChild(i).GetChild(j);
                tile.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                tile.GetComponent<TileGoUpDown>().CanGoUp = true;




            }
        }
    }
}
