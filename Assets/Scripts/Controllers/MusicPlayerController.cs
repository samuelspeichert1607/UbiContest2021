using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Photon.Pun;

public class MusicPlayerController : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private AudioSource track1;
    [SerializeField] private AudioSource track2;
    [SerializeField] private float startTime;
    [SerializeField] private float timeBeforeChange;
    [SerializeField] private float fadeTime;

    private MusicPlayerListener[] _musicPlayerListeners;
    private float currentTime;

    private bool isSwitched = false;
    private bool hasStarted = false;

    void Start()
    {
        currentTime = 0;
        mainMixer.SetFloat("musicVolume", 0f);
        UpdateMusicPlayerListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            currentTime += Time.deltaTime;
        }
        
        if (currentTime > startTime && !hasStarted)
        {
            track1.Play();
            StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", fadeTime, 0.7f));
            hasStarted = true;
        }
        if (currentTime >= timeBeforeChange && !isSwitched)
        {
            CrossfadeTracks(track1, track2);
            UpdateMusicPlayerListeners(); //Should be done on player instantiation, but is not working... so it will be here
            TriggerOnMusicChangeEvent();
            isSwitched = true;
        }
    }

    private void UpdateMusicPlayerListeners()
    {
        GameObject[] musicListeners = GameObject.FindGameObjectsWithTag("MusicPlayerListener");
        _musicPlayerListeners = new MusicPlayerListener[musicListeners.Length];
        for (int i = 0; i < musicListeners.Length; ++i)
        {
            _musicPlayerListeners[i] = musicListeners[i].GetComponent<MusicPlayerListener>();
        }
    }

    private void TriggerOnMusicChangeEvent()
    {
        foreach (MusicPlayerListener musicPlayerListener in _musicPlayerListeners)
        {
            musicPlayerListener.OnMusicChange();
        }
    }

    private void CrossfadeTracks(AudioSource currentTrack, AudioSource nextTrack)
    {
        nextTrack.Play();
        StartCoroutine(MixerFade.StartFade(mainMixer, "track1Volume", fadeTime, 0.0001f));
        StartCoroutine(MixerFade.StartFade(mainMixer, "track2Volume", fadeTime, 0.7f));
    }
}
