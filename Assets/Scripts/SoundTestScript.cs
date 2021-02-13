using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTestScript : MonoBehaviour
{
    /*
     * Ce script sera sûrement effacé dans le futur, ce n'est que pour tester les sons 
     */
    [SerializeField] private AudioSource musicClip;
    private AudioSource sfxTest;
    // Start is called before the first frame update
    void Start()
    {
        sfxTest = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("< Now playing SFX >");
            if(!sfxTest.isPlaying)
            {
                sfxTest.Play();
            }
        }

        else if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!musicClip.isPlaying)
            {
                Debug.Log("< Now playing music >");
                musicClip.Play();
            }
            else
            {
                Debug.Log("< Now stopping music >");
                musicClip.Stop();
            }
        }
    }
}
