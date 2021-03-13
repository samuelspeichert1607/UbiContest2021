using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGoUpDown : MonoBehaviour
{

    public Renderer tileRenderer;

    private Animation anim;
    private BoxCollider plateformeColider;
    private Color defaultColor;


    private void Start()
    {
        plateformeColider = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animation>();
        defaultColor = tileRenderer.material.color;
    }


    public void PlayAnimation()
    {
        anim.Play();
        plateformeColider.enabled = false;
        Invoke("ResetPlatform", 1);
        
    }

    private void ResetPlatform()
    {
        
        plateformeColider.enabled = true;
        tileRenderer.material.SetColor("_Color", defaultColor);
    }
}
