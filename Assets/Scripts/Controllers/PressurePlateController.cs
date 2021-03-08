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
        changeColor(Color.green);
        unlockedPlates = false;
        foreach (Actionable a in actionableObject)
        {
            a.OnAction();
        }
    }
    public void penality()
    {
        startPenalityTimer = true;
        changeColor(Color.red);
        unlockedPlates = false;

    }
    private void resetPlates()
    {
        changeColor(initialColor);
        timer = 0;
        unlockedPlates = true;
        startPenalityTimer = false;
    }

    private void changeColor(Color color)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_Color", color);
        }
    }
}
