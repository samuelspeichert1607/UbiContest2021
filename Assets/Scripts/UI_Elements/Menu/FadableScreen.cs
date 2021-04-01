using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadableScreen : MonoBehaviour
{
    [SerializeField] private Image coloredScreen;

    private bool isTransitionDone;
    private float _fadingTime;
    
    void Start()
    {
    }

    public void SetFadingTime(float fadingTime)
    {
        _fadingTime = fadingTime;
    }
    IEnumerator FadeTextToFullAlpha (Image i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,0);
        while (i.color.a < 1.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a + (Time.deltaTime / _fadingTime));
            yield return null;
        }
        isTransitionDone = true;
    }

    IEnumerator FadeTextToZeroAlpha (Image i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a - (Time.deltaTime / _fadingTime));
            yield return null;
        }

        isTransitionDone = true;
    }
    
    public void FadeToFullAlpha()
    {
        isTransitionDone = false;
        StartCoroutine(FadeTextToFullAlpha(coloredScreen));
    }
    
    public void FadeToZeroAlpha()
    {
        isTransitionDone = false;
        StartCoroutine(FadeTextToZeroAlpha(coloredScreen));
    }

    public void SetAlphaToZero()
    {
        coloredScreen.color = new Color(coloredScreen.color.r, coloredScreen.color.g, coloredScreen.color.b, 0);
    }
    
    public void SetAlphaToFull()
    {
        coloredScreen.color = new Color(coloredScreen.color.r, coloredScreen.color.g, coloredScreen.color.b, 255);
    }

    public bool IsTransitionDone()
    {
        return isTransitionDone;
    }
}
