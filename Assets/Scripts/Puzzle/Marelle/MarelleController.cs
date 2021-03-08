using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleController : MonoBehaviour
{
    [SerializeField] private Actionable[] actionableObject;
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
        //transform.GetChild(0).GetComponent<MovingWall>().CanMove = true;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
        for (int i = 0; i < transform.childCount; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }


    }

    public void gameLost()
    {
        hasCollisionUnlocked = false;
        for (int i = 0; i < transform.childCount ; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Transform tile = transform.GetChild(i).GetChild(j);
                if (i == 0)
                {
                    tile.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                }
                else
                {
                    tile.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    tile.GetComponent<TileGoUpDown>().CanGoDown = true;
                }
                
            }
        }
    }

    public void resetMarelle()
    {
        hasCollisionUnlocked = true;
        for (int i = 1; i < transform.childCount; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Transform tile = transform.GetChild(i).GetChild(j);
                tile.GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                tile.GetComponent<TileGoUpDown>().CanGoUp = true;




            }
        }
    }
}
