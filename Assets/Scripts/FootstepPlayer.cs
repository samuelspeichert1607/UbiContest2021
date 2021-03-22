using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using Random = UnityEngine.Random;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioSource audioSource;
    private PlayerController playerController;

    public void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void PlayFootsteps()
    {
        audioSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)], 0.05f);
    }

    public void ChangeMoveState()
    {
        playerController.ChangeCanMove();
    }
}
