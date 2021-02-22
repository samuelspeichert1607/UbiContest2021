using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ButtonLightPair
{
    public Lightbulb Lightbulb;
    public PressableButton PressableButton;
    public int pressingSequence = -1;
}
public class ButtonsTerminal : MonoBehaviour
{
    [SerializeField] private ButtonLightPair[] _buttonsLightPairs;
    [SerializeField] private float delayBeforeRetry;
    
    private int nextExpectedPressedButton;
    private bool isLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        nextExpectedPressedButton = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked)
        {
            CheckForButtonPress();
        }
    }

    private void CheckForButtonPress()
    {
        foreach (ButtonLightPair btnLightPair in _buttonsLightPairs)
        {
            if (btnLightPair.PressableButton.IsPressed())
            {
                Lightbulb lightbulb = btnLightPair.Lightbulb;
                if (IsPressedButtonCorrect(btnLightPair.pressingSequence))
                {
                    SetLightbulbToSuccess(lightbulb);
                    TerminalStepSuccess();
                }
                else if (!lightbulb.IsInSuccessState())
                {
                    TerminalFailure();
                }
            }
        }
    }

    private void TerminalStepSuccess()
    {
        nextExpectedPressedButton++;
        if (nextExpectedPressedButton == _buttonsLightPairs.Length)
        {
            Debug.Log("Game has succeeded!");
        }
    }

    private void TerminalFailure()
    {
        foreach (ButtonLightPair btnLightPair in _buttonsLightPairs)
        {
            btnLightPair.Lightbulb.Fail();
        }
        isLocked = true;
        Invoke("ResetTerminal", delayBeforeRetry);
    }

    private void SetLightbulbToSuccess(Lightbulb lightbulb)
    {
        lightbulb.Success();
    }

    private bool IsPressedButtonCorrect(int pressedIndex)
    {
        return pressedIndex == nextExpectedPressedButton;
    }

    private void ResetTerminal()
    {
        nextExpectedPressedButton = 0;
        isLocked = false;
        ResetLighbulbs();
    }

    private void ResetLighbulbs()
    {
        foreach (ButtonLightPair btnLightPair in _buttonsLightPairs)
        {
            btnLightPair.Lightbulb.Close();
        }
    }
}
