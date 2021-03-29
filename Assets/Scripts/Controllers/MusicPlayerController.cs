using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayerController : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private AudioSource track1;
    [SerializeField] private AudioSource track2;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBeforeChange;
    [SerializeField] private float fadeTime;

    private float currentTime;
    
    private bool isSwitched = false;
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
        // // Ceci fonctionne bien pour notre cas, mais ce serait à regler si les personnes décidaient de passer plus que 13 minutes dans le menu.
        if (currentTime >= timeBeforeChange && !isSwitched)
        {
            CrossfadeTracks(track1, track2);
            isSwitched = true;
        }
    }

    private void CrossfadeTracks(AudioSource currentTrack, AudioSource nextTrack)
    {
        nextTrack.Play();
        StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", fadeTime, 0.0001f));
        StartCoroutine(MixerFade.StartFade(mainMixer, "track2Volume", fadeTime, 0.7f));
    }
}
