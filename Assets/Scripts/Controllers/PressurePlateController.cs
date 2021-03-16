using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : MonoBehaviour
{

    [SerializeField] public Actionable[] actionableObject;
    [SerializeField] private float penalityTimer;
    public bool unlockedPlates = true;
    public bool startPenalityTimer = false;

    private float timer;
    private Color initialColor;

    void Start()
    {
        timer = penalityTimer;
        initialColor = GetComponentInChildren<Renderer>().material.color;
    }

    void Update()
    {
        if (startPenalityTimer)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                resetPlates();
            }
        }
    }
    public void won()
    {
        ChangeColor(Color.green);
        unlockedPlates = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
    }
    public void penality()
    {
        startPenalityTimer = true;
        ChangeColor(Color.red);
        unlockedPlates = false;

    }
    private void resetPlates()
    {
        ChangeColor(initialColor);
        timer = 0;
        unlockedPlates = true;
        startPenalityTimer = false;
    }

    private void ChangeColor(Color color)
    {
        foreach (Transform child in transform)
        {
            child.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
        }
    }
}
