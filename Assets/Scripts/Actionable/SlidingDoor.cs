using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class SlidingDoor : Actionable
{
    [SerializeField] private Vector3 movingDirection = new Vector3(1,0,0);

    public float movingDistance;

    [SerializeField] private bool isOpen;
    [SerializeField] private float movementSpeed = 1.0f;

    private bool isTranslating = false;

    private Vector3 startingPosition;
    private Vector3 destination;
    private float startTime;

    private Transform slide1;
    private Transform slide2;

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
            slide1.transform.position = Vector3.Lerp(startingPosition, startingPosition + (movingDirection * movingDistance), fractionOfTransition);
            slide2.transform.position = Vector3.Lerp(startingPosition, destination, fractionOfTransition);
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
