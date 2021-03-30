using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Main audio mixer
    [SerializeField] private AudioMixer mixer;

    // Minimum volume value below which the audio is muted
    private float minVolume = -49;
    public void SetMasterVolume(float newVolume)
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(newVolume) * 20);
    }

    public void SetMusicVolume(float newVolume)
    {
 
        mixer.SetFloat("musicVolume", Mathf.Log10(newVolume) * 20);
    }
    
    public void SetDialogVolume(float newVolume)
    {
 
        mixer.SetFloat("dialogVolume", Mathf.Log10(newVolume) * 20);
    }

    public void SetSfxVolume(float newVolume)
    {

        mixer.SetFloat("sfxVolume", Mathf.Log10(newVolume) * 20);
    }

    public void SetVcVolume(float newVolume)
    {
        mixer.SetFloat("vcVolume", Mathf.Log10(newVolume) * 20);

    }
}
