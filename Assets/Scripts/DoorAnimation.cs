using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorAnimation : MonoBehaviour
{
    public GameObject door;

    public AudioSource doorShutAudioSource;
    public AudioSource doorOpenAudioSource;

    public AudioClip doorShutClip;
    public AudioClip doorOpenSound;

    private void Start()
    {
        doorShutAudioSource.loop = false;
        doorOpenAudioSource.loop = false;

        doorShutAudioSource.clip = doorShutClip;
        doorOpenAudioSource.clip = doorOpenSound;
    }

    public void OpenDoorFast()
    {
        doorOpenAudioSource.Play();
        Vector3 openVector = new Vector3(door.transform.localEulerAngles.x, 173f, door.transform.localEulerAngles.z);
        door.transform.DOLocalRotate(openVector, 0.3f, RotateMode.FastBeyond360).OnComplete(() => PlayShutSound());
    }

    public void CloseDoorFast()
    {
        Vector3 openVector = new Vector3(door.transform.localEulerAngles.x, 0f, door.transform.localEulerAngles.z);
        door.transform.DOLocalRotate(openVector, 0.3f , RotateMode.FastBeyond360).OnComplete(() => PlayShutSound());
    }

    public void PlayShutSound()
    {
        doorShutAudioSource.Play();
    }

    public void PlayOpenSound()
    {
        doorOpenAudioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Patient"))
        {
            OpenDoorFast();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Patient"))
        {
            CloseDoorFast();
        }
    }
}
