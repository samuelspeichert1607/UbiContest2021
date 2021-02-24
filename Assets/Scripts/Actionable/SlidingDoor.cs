using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class SlidingDoor : Actionable
{
    [SerializeField] private Vector3 movingDirection;
        
    public float movingDistance;

    [SerializeField] private bool isOpen;
    [SerializeField] private float movementSpeed = 1.0f;
    // [SerializeField] private float transitionDuration = 3f;
    
    private bool isTranslating = false;

    private Vector3 startingPosition;
    private Vector3 destination;
    private float startTime;

    private void Start()
    {
        movingDirection = movingDirection.normalized;
    }

    private void Update()
    {
        if (isTranslating)
        {
            float fractionOfTransition = (Time.time - startTime) * movementSpeed / movingDistance;
            transform.position = Vector3.Lerp(startingPosition, destination, fractionOfTransition);
            if (fractionOfTransition >= 1)
            {
                isTranslating = false;
            }
        }
    }

    public override void OnAction()
    {
        if (isOpen)
        {
            startTime = Time.time;
            startingPosition = transform.position;
            destination = startingPosition + (movingDirection * movingDistance);
            isTranslating = true;
        }
        else
        {
            startTime = Time.time;
            startingPosition = transform.position;
            destination = startingPosition + (-movingDirection * movingDistance);
            isTranslating = true;
        }
    }
}
