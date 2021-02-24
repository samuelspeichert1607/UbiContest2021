using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleWon : MonoBehaviour
{
    public bool isResolve =false;
    public float timerTime;
    public bool unlockCollision =true;

    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        unlockCollision = true;
    }
    public void gameWon()
    {
        unlockCollision = false;
        transform.GetChild(0).GetComponent<MovingWall>().CanMove = true;
        for (int i = 1; i < transform.childCount - 2; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
        }


    }
}
