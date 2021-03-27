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
    [SerializeField] private PairOfPlates[] pressurePlatesPairs;

    private GameObject sourcePlate = null;
    private bool isLocked = true;

    private bool testBool =true;
    
    private Color successColor = Color.green;
    private Color pressedColor = Color.yellow;
    private void Start()
    {
    }

    private void Update()
    {
        UpdateAllPlatesState();

        //cheat code
        // if (Input.GetButtonDown("Fire2") && testBool)
        // {
        //     OnSuccess();
        //     testBool = false;
        // }
        
    }

    private void UpdateAllPlatesState()
    {
        foreach (var platePair in pressurePlatesPairs)
        {
            UpdatePlateState(platePair.plate1);
            UpdatePlateState(platePair.plate2);
            if (platePair.plate1.IsPressedAndUnlocked() && platePair.plate2.IsPressedAndUnlocked())
            {
                OnSuccess();
            }
        }
    }

    private void UpdatePlateState(PressurePlateButtonSync plate)
    {
        if (!plate.IsLocked())
        {
            if (plate.IsPressedAndUnlocked())
            {
                PlateHaveBeenPressed(plate);
            }
            else
            {
                plate.Reset();
            }   
        }
    }

    private void PlateHaveBeenPressed(PressurePlateButtonSync plate)
    {
        plate.SetColor(pressedColor);
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
