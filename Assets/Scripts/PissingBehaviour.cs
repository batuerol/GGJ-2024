using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PissingBehaviour : MonoBehaviour
{
    public GameObject censorObject;
    public ParticleSystem pissParticle;

    ParticleSystem.MainModule particleModule;
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
        censorObject.SetActive(true);
        pissParticle.Play();
        DOTween.To(() => particleModule.gravityModifierMultiplier, x => particleModule.gravityModifierMultiplier = x, 0.4f, 1f);
    }

}
