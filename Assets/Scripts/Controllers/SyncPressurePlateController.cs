using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public class PairOfPlates
{
    [SerializeField] public PressurePlateButtonSync plate1;
    [SerializeField] public PressurePlateButtonSync plate2;
}

public class SyncPressurePlateController : MonoBehaviour
{
    [SerializeField] private Actionable[] actionableObject;
    [SerializeField] float pressTime = 2;
    [SerializeField] float penalityTime = 5;
    [SerializeField] private PairOfPlates[] pressurePlatesPairs; 
    private float timer;
    private bool isTimerStarted = false;

    private GameObject sourcePlate = null;
    private bool isLocked = true;

    private bool testBool =true;
    
    private Color successColor = Color.green;
    private Color failureColor = Color.red;
    private Color pressedColor = Color.yellow;
    private void Start()
    {
        timer = pressTime;
    }

    private void Update()
    {
        if (isTimerStarted)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                isTimerStarted = false;
                timer = 0;
                OnFailure();

            }
        }

        CheckIfAnyPairOfPlateIsPressed();

        //cheat code
        if (Input.GetButtonDown("Fire2") && testBool)
        {
            OnSuccess();
            testBool = false;
        }
        
    }

    private void CheckIfAnyPairOfPlateIsPressed()
    {
        foreach (var platePair in pressurePlatesPairs)
        {
            if (platePair.plate1.IsPressedAndUnlocked())
            {
                if (platePair.plate2.IsPressedAndUnlocked())
                {
                    OnSuccess();
                }
                else
                {
                    PlateHaveBeenPressed(platePair.plate1);
                }
            }

            if (platePair.plate2.IsPressedAndUnlocked())
            {
                if (platePair.plate1.IsPressedAndUnlocked())
                {
                    OnSuccess();
                }
                else
                {
                    PlateHaveBeenPressed(platePair.plate2);
                }
            }
        }
    }

    private void PlateHaveBeenPressed(PressurePlateButtonSync plate)
    {
        if (!isTimerStarted)
        {
            timer = pressTime;
            isTimerStarted = true;
            plate.SetColor(pressedColor);
        }
    }

    private void OnFailure()
    {
        SetAllPlatesColor(failureColor);
        LockPuzzle();
        Invoke(nameof(Reset), penalityTime);
    }

    private void OnSuccess()
    {
        SetAllPlatesColor(successColor);
        LockPuzzle();
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
    }

    private void LockPuzzle()
    {
        isLocked = true;
        foreach (var platePair in pressurePlatesPairs)
        {
            platePair.plate1.Lock();
            platePair.plate2.Lock();
        }
    }
    
    private void UnlockPuzzle()
    {
        isLocked = true;
        foreach (var platePair in pressurePlatesPairs)
        {
            platePair.plate1.Unlock();
            platePair.plate2.Unlock();
        }
    }
    
    private void Reset()
    {
        UnlockPuzzle();
        foreach (var platePair in pressurePlatesPairs)
        {
            platePair.plate1.Reset();
            platePair.plate2.Reset();
        }
    }

    private void SetAllPlatesColor(Color color)
    {
        foreach (var platePair in pressurePlatesPairs)
        {
            platePair.plate1.SetColor(color);
            platePair.plate2.SetColor(color);
        }
    }
}
