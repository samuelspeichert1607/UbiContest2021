using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleController : MonoBehaviour
{
    [SerializeField] private Actionable[] actionableObject;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource lossSound;
    public bool isResolve =false;
    public float timerTime;
    public bool hasCollisionUnlocked =true;
    private Color initialColor;

    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        hasCollisionUnlocked = true;
        initialColor = GetComponentInChildren<Renderer>().material.color;

    }
    public void gameWon()
    {
        winSound.Play();
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
                transform.GetChild(i).GetChild(j).GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green);
            }
        }


    }

    public void gameLost()
    {
        lossSound.Play();
        hasCollisionUnlocked = false;
        for (int i = 0; i < transform.childCount ; i++)
        {

            for (int j = 0; j < transform.GetChild(i).childCount; j++)
            {
                Transform tile = transform.GetChild(i).GetChild(j);
                if (i == 0)
                {
                    tile.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.cyan);
                }
                else
                {
                    tile.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
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
                tile.GetComponentInChildren<Renderer>().material.SetColor("_Color", initialColor);
                tile.GetComponent<TileGoUpDown>().CanGoUp = true;




            }
        }
    }
}
