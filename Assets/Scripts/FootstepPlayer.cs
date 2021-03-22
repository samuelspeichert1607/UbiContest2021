using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioSource audioSource;


    public void PlayFootsteps()
    {
        audioSource.PlayOneShot(footstepSounds[Random.Range(0, footstepSounds.Length)], 0.5f);
    }
}
