using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadableText : MonoBehaviour
{
    private TextMeshProUGUI textBox;

    private bool isTransitionDone;
    
    void Start()
    {
        textBox = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    
    IEnumerator FadeTextToFullAlpha (TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,0);
        while (i.color.a < 1.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a + (Time.deltaTime / 3f));
            yield return null;
        }
        isTransitionDone = true;
    }

    IEnumerator FadeTextToZeroAlpha (TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a - (Time.deltaTime / 2f));
            yield return null;
        }

        isTransitionDone = true;
    }
    
    public void FadeToFullAlpha()
    {
        isTransitionDone = false;
        StartCoroutine(FadeTextToFullAlpha(textBox));
    }
    
    public void FadeToZeroAlpha()
    {
        isTransitionDone = false;
        StartCoroutine(FadeTextToZeroAlpha(textBox));
    }

    public void SetAlphaToZero()
    {
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 0);
    }
    
    public void SetAlphaToFull()
    {
        textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, 255);
    }

    public bool IsTransitionDone()
    {
        return isTransitionDone;
    }
}
