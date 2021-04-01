using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class menuMusicController : MonoBehaviour
{

    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private AudioSource track1;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBeforeChange;
    [SerializeField] private float fadeTime;

    private float currentTime;
    private bool hasStarted = false;

    void Start()
    {
        currentTime = 0;
        mainMixer.SetFloat("musicVolume", 0f);
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;


        if (currentTime > startTime && !hasStarted)
        {
            track1.Play();
            StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", fadeTime, 0.7f));
            hasStarted = true;
        }

    }
}


