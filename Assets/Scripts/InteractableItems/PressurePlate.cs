using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private AudioClip plateDownSound;
    [SerializeField] 
    private AudioClip plateUpSound;
    [SerializeField]
    private AudioSource audioSource;
    private float speed=5;
    private float heightDifferenceWhenDown = 2;
    private bool goUp = false;
    private bool goDown = false;
    void Update()
    {
        if (goUp)
        {
            if (transform.localPosition.y < 0)
            {
                transform.localPosition += new Vector3(0, Time.deltaTime * speed, 0);
            }
           
            else
            {

                goUp = false;
            }
        }
        else if (goDown)
        {
            if (transform.localPosition.y > -heightDifferenceWhenDown)
            {
                transform.localPosition -= new Vector3(0, Time.deltaTime * speed, 0);
            }
            
            else
            {
                goDown = false;
            }
        }
    }

    public void CollisionDetected()
    {
        audioSource.PlayOneShot(plateDownSound, 0.7f);
        goDown = true;
        CollisionEntered();
    }

    public void CollisionExited()
    {
        audioSource.PlayOneShot(plateUpSound, 0.7f);
        goUp = true;


    }

    public virtual void CollisionEntered()
    {

        
    }
}
