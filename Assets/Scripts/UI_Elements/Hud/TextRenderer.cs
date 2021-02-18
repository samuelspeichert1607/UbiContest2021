using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRenderer : MonoBehaviour
{
    [SerializeField] private GameObject textContainer;
    private TextMeshProUGUI textField;

    public void Start()
    {
        textField = textContainer.GetComponent<TextMeshProUGUI>();
        
        textContainer.SetActive(false);
    }

    public void ShowInfoText(string info)
    {
        textContainer.SetActive(true);
        textField.text = info;
    }

    public void CloseInfoText()
    {
        textContainer.SetActive(false);
        textField.text = "";
    }
    
    
}
