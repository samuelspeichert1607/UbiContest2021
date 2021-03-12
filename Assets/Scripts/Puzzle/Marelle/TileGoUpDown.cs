using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGoUpDown : MonoBehaviour
{
    //'a modifier quand j,aurai un prefab;
    //public bool CanGoUp = false;
    //public bool CanGoDown = false;
    public Renderer tileRenderer;

    private Animation anim;
    private BoxCollider plateformeColider;



    private void Start()
    {
        plateformeColider = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animation>();

    }


    public void PlayAnimation()
    {
        anim.Play();
        plateformeColider.enabled = false;
        Invoke("EnableCollider", 1);
        
    }

    private void EnableCollider()
    {
        plateformeColider.enabled = true;
    }
}
