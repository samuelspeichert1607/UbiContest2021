using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerMenu : MonoBehaviour
{
    [SerializeField] private AudioClip buttonNavigationSound;
    [SerializeField] private AudioClip buttonClickedSound;
    
    [SerializeField] private AudioSource audioSource;
    
    
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(buttonClickedSound);
    }

    public void PlayButtonNavigationSound()
    {
        audioSource.PlayOneShot(buttonNavigationSound);
    }
}
