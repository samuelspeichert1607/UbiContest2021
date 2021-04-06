using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MarelleController : MonoBehaviour
{
    [SerializeField] private RobotVoiceController robotFail;

    [SerializeField]
    private Actionable[] actionableObject;

    [SerializeField]
    private AudioClip winSound;

    [SerializeField]
    private AudioClip lossSound;

    private AudioSource audioSource;
    
    public bool isResolve = false;

    public float timerTime;


    private PhotonView _photonView;


    private void Start() 
    {
        _photonView = GetComponent<PhotonView>();
        audioSource = GetComponent<AudioSource>();
    }
    public void gameWon()
    {
        audioSource.PlayOneShot(winSound, 0.7f);

        foreach (Actionable a in actionableObject)
        {
            if (!a.hasActioned)
            {
                a.OnAction();
            }
        }

        // _photonView.RPC("rcpGameWon", RpcTarget.All);

    }
    //[PunRPC]
    //private void rcpGameWon()
    //{
    //    audioSource.PlayOneShot(winSound, 0.7f);
    //    hasCollisionUnlocked = false;
    //    foreach (Actionable a in actionableObject)
    //    {
    //        if (!a.hasActioned)
    //        {
    //            a.OnAction();
    //        }
    //    }


    //}
    public void gameLost()
    {
         _photonView.RPC(nameof(rcpGameLost), RpcTarget.All);
        //audioSource.PlayOneShot(lossSound, 0.7f);
        //if (UnityEngine.Random.Range(0, 2) == 0)//50%
        //{
        //    robotFail.PlayTaskFailed();
        //}
        //foreach (Transform child in transform)
        //{

        //    foreach (Transform toddler in child)
        //    {

        //        Material tileMat = toddler.GetComponentInChildren<TileGoUpDown>().tileRenderer.material;

        //        tileMat.SetColor("_Color", Color.red);

        //        toddler.GetComponent<TileGoUpDown>().PlayAnimation();

        //    }
        //}
    }

    [PunRPC]
    public void rcpGameLost()
    {
        audioSource.PlayOneShot(lossSound, 0.7f);
        if (UnityEngine.Random.Range(0, 2) == 0)//50%
        {
            robotFail.PlayTaskFailed();
        }
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
