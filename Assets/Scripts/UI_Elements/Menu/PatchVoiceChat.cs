using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatchVoiceChat : MonoBehaviour
{
    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        float _sliderStartValue = _slider.value;
        _slider.SetValueWithoutNotify(_sliderStartValue - 0.01f);
        _slider.SetValueWithoutNotify(_sliderStartValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
