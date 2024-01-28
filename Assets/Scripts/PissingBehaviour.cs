using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PissingBehaviour : MonoBehaviour
{
    public GameObject censorObject;
    public ParticleSystem pissParticle;

    ParticleSystem.MainModule particleModule;

    public AudioSource zipAudiosource;
    public AudioClip zipClip;
    // Start is called before the first frame update
    void Start()
    {
        censorObject.SetActive(false);
        particleModule = pissParticle.main;
        particleModule.gravityModifier = 1;
        pissParticle.Stop();
    }
    // Update is called once per frame

    public void StartParticleSystem()
    {
        zipAudiosource.clip = zipClip;
        zipAudiosource.Play();
        censorObject.SetActive(true);
        Invoke("StartPissing", 1.5f);
    }

    public void StartPissing()
    {
        pissParticle.Play();
        DOTween.To(() => particleModule.gravityModifierMultiplier, x => particleModule.gravityModifierMultiplier = x, 0.3f, 1f);
    }

}
