using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressurePlate : MonoBehaviour
{
    [SerializeField]
    private AudioClip plateDownSound;
    [SerializeField] 
    private AudioClip plateUpSound;
    [SerializeField]
    private AudioSource audioSource;
    private float speed=5;
    private float heightDifferenceWhenDown = 2;
    public bool goUp = false;
    public bool goDown = false;

    void Start()
    {
        // audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (goUp)
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.localPosition.y >= 0)
            {

                goUp = false;
            }
        }
        else if (goDown)
        {

            transform.localPosition -= new Vector3(0, Time.deltaTime * speed, 0);
            if (transform.localPosition.y <= -heightDifferenceWhenDown)
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

    public abstract void CollisionEntered();
}