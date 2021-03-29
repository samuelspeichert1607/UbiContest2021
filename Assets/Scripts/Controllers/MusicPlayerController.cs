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
    // [SerializeField] private AudioClip track1Clip;
    // [SerializeField] private AudioClip track2Clip;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBeforeChange;

    private bool isSwitched = false;
    private bool hasStarted = false;

    void Start()
    {
        mainMixer.SetFloat("musicVolume", 0f);
        // track1.clip = track1Clip;
        // track2.clip = track2Clip;
        // track1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime && !hasStarted)
        {
            track1.Play();
            StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", 2, 1f));
            hasStarted = true;
        }
        // // Ceci fonctionne bien pour notre cas, mais ce serait à regler si les personnes décidaient de passer plus que 13 minutes dans le menu.
        if (Time.time >= timeBeforeChange && !isSwitched)
        {
            CrossfadeTracks(track1, track2);
            isSwitched = true;
        }
    }

    private void CrossfadeTracks(AudioSource currentTrack, AudioSource nextTrack)
    {
        nextTrack.Play();
        StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", 2, 0.0001f));
        StartCoroutine(MixerFade.StartFade(mainMixer, "track2Volume", 2, 1));
    }
}
