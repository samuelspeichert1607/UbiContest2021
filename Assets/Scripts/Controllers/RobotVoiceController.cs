using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RobotVoiceController : MonoBehaviour
{
    [SerializeField] private AudioClip introClip;
    [SerializeField] private AudioClip winClip;
    [SerializeField] private AudioClip lostClip;
    [SerializeField] private AudioClip[] taskFail;
    private float startTime;
    private bool canPlaySound=true;
    private AudioSource audioSource;
    private AudioClip currentClip;

    void Start()
    {
        currentClip = introClip;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (canPlaySound && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            canPlaySound = false;
            PlayClip(introClip);
        }

    }

 
    public void PlayTaskFailed()
    {
        PlayClip(taskFail[UnityEngine.Random.Range(0, taskFail.Length)]);
    }

    public void PlayWin()
    {
        PlayClip(winClip);
    }
    public void PlayLost()
    {
        PlayClip(lostClip);
    }

    private void PlayClip(AudioClip clip)
    {
        if (isNotPlaying() || clip == introClip)
        {
            currentClip = clip;
            audioSource.PlayOneShot(clip, 1);
            startTime = Time.time;
        }

        //foreach (Transform speaker in transform)
        //{
        //    speaker.GetComponent<AudioSource>().PlayOneShot(clip, 1);

        //}
    }
    private bool isNotPlaying()
    {
        return Time.time - startTime >= currentClip.length;
    }
}
