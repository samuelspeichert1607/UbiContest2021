using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PulsateText : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    [SerializeField] private float pulsateTimer = 1f;
    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(nameof(Pulsate));
    }

    IEnumerator Pulsate ()
    {
        while (true) {
            StartCoroutine(FadeTextToFullAlpha(textBox));
            yield return new WaitForSeconds(pulsateTimer);
            StartCoroutine(FadeTextToZeroAlpha(textBox));
            yield return new WaitForSeconds(pulsateTimer);
        }
        
    }
    IEnumerator FadeTextToFullAlpha (TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,0);
        while (i.color.a < 1.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a + (Time.deltaTime / 1f));
            yield return null;
        }
    }

    IEnumerator FadeTextToZeroAlpha (TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r,i.color.g,i.color.b,1);
        while (i.color.a > 0.0f) {
            i.color = new Color(i.color.r,i.color.g,i.color.b,i.color.a - (Time.deltaTime / 1f));
            yield return null;
        }
    }
    
    public void SetFastPulse()
    {
        pulsateTimer /= 4;
        StopAllCoroutines();
        StartCoroutine(nameof(Pulsate));
    }
    
}
