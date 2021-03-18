using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[Serializable]
public class MarelleAndPressurePlate
{
    public PressurePlateButtonSync pressurePlate;
    public Platform marellePlatform;
}

public class MarelleAndPlateController : MonoBehaviour
{
    [SerializeField]
    private MarelleAndPressurePlate[] marelleAndPlatePairs;

    [SerializeField] private PressurePlateButtonSync[] invalidPlates;
    
    [SerializeField] private Actionable[] actionableObject;

    [SerializeField] private AudioClip winSound;

    [SerializeField] private AudioClip lossSound;

    private AudioSource audioSource;

    [SerializeField] private float penalityTime;
    [SerializeField] private float pressTime;
    private bool hasTimerStarted;
    private float timerStart;
    
    private bool puzzleIsLocked = false;
    private bool hasPressedAnInvalidPlate = false;
    
    private Color pressedColor = Color.yellow;
    private Color failureColor = Color.red;
    private Color successColor = Color.green;

    private bool isWaitingForPairedItemToBePressed;
    private MarelleAndPressurePlate pressedPair;
    private int correctPressedPairCount = 0;
    
    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        SetPlatformsPenalityTime();
    }

    private void SetPlatformsPenalityTime()
    {
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            marelleAndPlatePair.marellePlatform.SetPressTime(pressTime);
        }
    }

    void Update()
    {
        if (!puzzleIsLocked)
        {
            if (hasTimerStarted)
            {
                if (Time.time > timerStart + pressTime)
                {
                    Penality();
                }
            }
            CheckIfAnyInvalidPlateIsPressed();
            CheckIfAnyValidPlateIsPressed();   
        }

        if (correctPressedPairCount == marelleAndPlatePairs.Length)
        {
            GameWon();
        }
    }

    private void CheckIfAnyValidPlateIsPressed()
    {
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            var currentPlatform = marelleAndPlatePair.marellePlatform;
            var currentPlate = marelleAndPlatePair.pressurePlate;

            if (currentPlate.IsPressedAndUnlocked())
            {
                if (isWaitingForPairedItemToBePressed)
                {
                    if (currentPlate == pressedPair.pressurePlate)
                    {
                        PairHasBeenCorrectlyPressed(marelleAndPlatePair);
                    }
                }
                else
                {
                    pressedPair = marelleAndPlatePair;
                    currentPlate.SetColor(pressedColor);
                    isWaitingForPairedItemToBePressed = true;
                    timerStart = Time.time;
                }
            }
            if (currentPlatform.IsPressedAndUnlocked())
            {
                if (isWaitingForPairedItemToBePressed)
                {
                    if (currentPlatform == pressedPair.marellePlatform)
                    {
                        PairHasBeenCorrectlyPressed(marelleAndPlatePair);
                    }
                }
                else
                {
                    pressedPair = marelleAndPlatePair;
                    currentPlatform.SetColor(pressedColor);
                    isWaitingForPairedItemToBePressed = true;
                    timerStart = Time.time;
                    break;
                }
            }

        }
    }

    private void PairHasBeenCorrectlyPressed(MarelleAndPressurePlate marelleAndPlatePair)
    {
        marelleAndPlatePair.marellePlatform.SetColor(successColor);
        marelleAndPlatePair.pressurePlate.SetColor(successColor);
        isWaitingForPairedItemToBePressed = false;
        correctPressedPairCount++;
    }

    private void CheckIfAnyInvalidPlateIsPressed()
    {
        foreach (var plate in invalidPlates)
        {
            if (plate.IsPressedAndUnlocked())
            {
                if (isWaitingForPairedItemToBePressed || hasPressedAnInvalidPlate)
                {
                    Penality();
                }
                else
                {
                    hasPressedAnInvalidPlate = true;
                    plate.SetColor(pressedColor);
                    timerStart = Time.time;
                    hasTimerStarted = true;
                }
            }
        }
    }

    public void Penality()
    {
        audioSource.PlayOneShot(lossSound, 0.7f);
        
        LockPuzzle();
        SetAllPlatesAndPlatformsColor(failureColor);
        
        Invoke(nameof(UnlockPuzzle), penalityTime);
        ResetPuzzle();
    }

    private void SetAllPlatesAndPlatformsColor(Color color)
    {
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            marelleAndPlatePair.marellePlatform.SetColor(color);
            marelleAndPlatePair.pressurePlate.SetColor(color);
        }
        foreach (var plate in invalidPlates)
        {
            plate.SetColor(color);
        }
    }

    private void LockPuzzle()
    {
        puzzleIsLocked = true;
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            marelleAndPlatePair.marellePlatform.Lock();
            marelleAndPlatePair.pressurePlate.Lock();
        }
        foreach (var plate in invalidPlates)
        {
            plate.Lock();
        }
    }

    private void UnlockPuzzle()
    {
        puzzleIsLocked = false;
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            marelleAndPlatePair.marellePlatform.Unlock();
            marelleAndPlatePair.pressurePlate.Unlock();
        }
        foreach (var plate in invalidPlates)
        {
            plate.Unlock();
        }
    }

    private void ResetPuzzle()
    {
        puzzleIsLocked = false;
        isWaitingForPairedItemToBePressed = false;
        hasTimerStarted = false;
        hasPressedAnInvalidPlate = false;
        correctPressedPairCount = 0;
        foreach (var marelleAndPlatePair in marelleAndPlatePairs)
        {
            marelleAndPlatePair.marellePlatform.Reset();
            marelleAndPlatePair.pressurePlate.Reset();
        }
        foreach (var plate in invalidPlates)
        {
            plate.Reset();
        }
    }

    public void GameWon()
    {
        audioSource.PlayOneShot(winSound, 0.7f);
        SetAllPlatesAndPlatformsColor(successColor);
        
        foreach (Actionable a in actionableObject)
        {
            Debug.Log("Congrat you have won");
            a.OnAction();
        }

    }

}
