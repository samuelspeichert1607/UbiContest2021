using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMenu : MonoBehaviour
{
    [SerializeField] private AudioClip buttonNavigationSound;
    [SerializeField] private AudioClip buttonClickedSound;
    
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(buttonClickedSound);
    }

    public void PlayButtonNavigationSound()
    {
        _audioSource.PlayOneShot(buttonNavigationSound);
    }
}
