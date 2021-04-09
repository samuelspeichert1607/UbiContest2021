using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class SlidingDoor : Actionable
{
   
    [SerializeField] private Vector3 movingDirection = new Vector3(1,0,0);

    public float movingDistance =1;

    [SerializeField] private bool isOpen;
    [SerializeField] private float movementSpeed = 1.0f; 
    
    [SerializeField] private AudioClip openSoundClip;
    [SerializeField] private AudioClip closeSoundClip;

    [SerializeField] private AudioSource audioSource;
    
    private bool isTranslating = false;

    private Vector3 startingPosition;
    private Vector3 destination;
    private float startTime;

    private Transform slide1;
    private Transform slide2;
    private Vector3 startPosSlide1;
    private void Start()
    {
        slide1 = transform.GetChild(1);
        slide2 = transform.GetChild(2);
    }

    private void Update()
    {
        if (isTranslating)
        {
            float fractionOfTransition = (Time.time - startTime) * movementSpeed / movingDistance;
            slide1.transform.position = Vector3.Lerp(startPosSlide1, startingPosition + (movingDirection * movingDistance), fractionOfTransition);
            slide2.transform.position = Vector3.Lerp(startingPosition, destination, fractionOfTransition);

            
            if (fractionOfTransition >= 1)
            {
                isTranslating = false;
            }
        }
    }

    public override void OnAction()
    {
        hasActioned = true;
        if (isOpen)
        {
            isOpen = false;
            startTime = Time.time;
            startingPosition = transform.position- (movingDirection * movingDistance);
            startPosSlide1 = transform.position + (movingDirection * movingDistance);
            destination = startingPosition + (movingDirection * movingDistance);
            isTranslating = true;
            PlayCloseSound();
        }
        else
        {
            isOpen = true;
            startTime = Time.time;
            startingPosition = transform.position;
            startPosSlide1 = transform.position;
            destination = startingPosition + (-movingDirection * movingDistance);
            isTranslating = true;
            PlayOpenSound();
        }
    }

    private void PlayOpenSound()
    {
        audioSource.PlayOneShot(openSoundClip);
    }

    private void PlayCloseSound()
    {
        audioSource.PlayOneShot(closeSoundClip);
    }
}



