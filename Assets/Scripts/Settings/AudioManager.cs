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
    public void setMasterVolume(float newVolume)
    {
        if (newVolume >= minVolume)
        {
            mixer.SetFloat("masterVolume", newVolume);
        }
        else
        {
            // Until proven otherwise, setting volume to -80 dB is the best way to mute a channel!
            mixer.SetFloat("masterVolume", -80);
        }
    }

    public void setMusicVolume(float newVolume)
    {
        if (newVolume >= minVolume)
        {
            mixer.SetFloat("musicVolume", newVolume);
        }
        else
        {

            mixer.SetFloat("musicVolume", -80);
        }
    }

    public void setSfxVolume(float newVolume)
    {
        if (newVolume >= minVolume)
        {
            mixer.SetFloat("sfxVolume", newVolume);
        }
        else
        {
            mixer.SetFloat("sfxVolume", -80);
        }
    }
}
