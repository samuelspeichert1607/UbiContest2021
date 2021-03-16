using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarelleController : MonoBehaviour
{
    [SerializeField]
    private Actionable[] actionableObject;

    [SerializeField]
    private AudioClip winSound;

    [SerializeField]
    private AudioClip lossSound;

    private AudioSource audioSource;
    
    public bool isResolve = false;

    public float timerTime;

    public bool hasCollisionUnlocked =true;
    
    private void Start() //sinon il est 'a false et je ne sais pas pourquoi
    {
        hasCollisionUnlocked = true;
        audioSource = GetComponent<AudioSource>();
    }

    public void gameWon()
    {
        audioSource.PlayOneShot(winSound, 0.7f);
        hasCollisionUnlocked = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }

    }

    public void gameLost()
    {
        audioSource.PlayOneShot(lossSound, 0.7f);
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
