using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class text : MonoBehaviour
{
    [SerializeField] private float waitTimer = 7f;
    List<string> texts = new List<string>();
    public BoiteText boite;
    private TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        // Ajoute la phrase au dialogue
        foreach (string sentence in boite.phrase) {
            texts.Add(sentence);
        }
        textBox = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine("textChange");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    IEnumerator textChange() {
        int compteur = 0;
        while (true) {
            
            textBox.text = texts[compteur];
            StartCoroutine(FadeTextToFullAlpha(textBox));
            
            yield return new WaitForSeconds(waitTimer-1);
            StartCoroutine(FadeTextToZeroAlpha(textBox));
            yield return new WaitForSeconds(1);
            if (compteur == texts.Count-1) {
                compteur = 0;
            }
            else {
                compteur++;
            }   
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
}
