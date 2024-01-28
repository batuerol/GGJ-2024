using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public void PlayAudio(AudioClip clip)
    {
        AudioSource availableSource = audioSource1;
        if (audioSource1.isPlaying)
        {
            availableSource = audioSource2;
        }

        availableSource.clip = clip;
        availableSource.Play();
    }
}
