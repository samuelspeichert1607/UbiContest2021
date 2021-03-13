using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleController : MonoBehaviour
{
    [SerializeField] private Actionable[] actionableObject;
    public float timerTime;
    public bool hasCollisionUnlocked =true;


    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        hasCollisionUnlocked = true;
 
    }
    public void gameWon()
    {
        hasCollisionUnlocked = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
        foreach (Transform child in transform)
        {

            foreach (Transform toddler in child)
            {
                toddler.GetComponentInChildren<TileGoUpDown>().tileRenderer.material.SetColor("_Color", Color.green);
            }
        }


    }

    public void gameLost()
    {
        hasCollisionUnlocked = false;
        foreach (Transform child in transform)
        {

            foreach (Transform toddler in child)
            {

                Material tileMat = toddler.GetComponentInChildren<TileGoUpDown>().tileRenderer.material;

                tileMat.SetColor("_Color", Color.red);

                toddler.GetComponent<TileGoUpDown>().PlayAnimation();

            }
        }
    }


}
