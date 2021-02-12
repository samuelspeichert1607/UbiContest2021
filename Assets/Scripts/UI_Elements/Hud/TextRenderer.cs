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

    public void ShowInfo(string info)
    {
        textContainer.SetActive(true);
        textField.text = info;
    }

    public void CloseText()
    {
        textContainer.SetActive(false);
        textField.text = "";
    }
    
    
}
