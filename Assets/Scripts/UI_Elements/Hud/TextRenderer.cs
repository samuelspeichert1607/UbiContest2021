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
        if(textContainer != null && textField != null)
        {
            textContainer.SetActive(true);
            textField.text = info;
        }
    }

    public void CloseInfoText()
    {
        if (textContainer != null && textField != null)
        {
            textContainer.SetActive(false);
            textField.text = "";
        }
    }

    public bool IsClosed()
    {
        return !textContainer.activeSelf;
    }
    
    
}
