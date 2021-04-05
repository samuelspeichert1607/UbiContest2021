using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenShortage : MonoBehaviour, MusicPlayerListener
{

    [SerializeField] private PulsateText oxygenText;
    [SerializeField] private Image fillBar;
    [SerializeField] private float pulsatingTextTime;
    [SerializeField] private Color onShortageFillBarColor;

    // Start is called before the first frame update
    void Start()
    {
        oxygenText.StartCoroutine(nameof(Start));
        oxygenText.SetAlphaToFull();
    }

    public void OnMusicChange()
    {
        fillBar.color = onShortageFillBarColor;
        oxygenText.StartPulsating();
        Invoke(nameof(StopOxygenTextPulsation), pulsatingTextTime);
        Debug.Log("OnMusicChange!");
    }

    private void StopOxygenTextPulsation()
    {
        oxygenText.StopAllCoroutines();
        oxygenText.SetAlphaToFull();
    }
}
