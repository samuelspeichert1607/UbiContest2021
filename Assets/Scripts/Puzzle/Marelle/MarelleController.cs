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


    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        hasCollisionUnlocked = true;
 
    }
    public void gameWon()
    {
        winSound.Play();
        hasCollisionUnlocked = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }

    }

    public void gameLost()
    {
        lossSound.Play();
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
