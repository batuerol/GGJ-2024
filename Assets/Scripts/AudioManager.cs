using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
