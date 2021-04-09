using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    [SerializeField] private float loadDelay;
    [SerializeField] private AudioClip pressSound;
    [SerializeField] private PulsateText pulsateText;
    
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        pulsateText.StartPulsating();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            pulsateText.SetFastPulse();
            _audioSource.PlayOneShot(pressSound);
            Invoke(nameof(LoadMenu), loadDelay);
        }
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
