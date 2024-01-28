using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatientController : MonoBehaviour
{

    public NavMeshAgent agent;
    public Animator animator;

    public GameObject removableObject; //balta gibi objeler burada
    public Transform patientTargetTransform;
    public Transform patientExitTransform;

    [Header("Animator Strings")]
    public string walkString = "isWalking";
    public string idleString;
    public string deadString = "isDead";

    [Header("Rotating Object")]
    public RotatingObjects rotationBehaviour;

    [Header("Object Activator")]
    public ObjectActivator objectActivator;

    [Header("Piss Behaviour")]
    public PissingBehaviour pissbehaviour;


    [Header("Patient Story")]
    public string story;
    public string thanksString;

    public PatienteState state;

    public bool startCheckDistance = false;

    public bool doctorDestinationSet = false;
    public bool exitDestinationSet = false;

    public bool reachedDoctor = false;
    public bool reachedExit = false;

    private Action OnPatientExit;

    public bool patientServed = false;

    [Header("Audio Clips")]
    public AudioClip speakClip;
    public AudioClip actionSound;

    private void Start()
    {
        //agent.isStopped = true;
        OnPatientExit += GameManager.Instance.CallNextPatient;
    }
    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case PatienteState.WALK:
                DisableAllBools();
                animator.SetBool(walkString, true);
                break;
            case PatienteState.IDLE:
                break;
            case PatienteState.DEAD:
                break;
            default:
                break;
        }

        if (doctorDestinationSet)
        {
            if (!reachedDoctor)
            {
                CheckReachDestinationForDoctor();
            }
        }

        if (exitDestinationSet)
        {
            if (!reachedExit)
            {
                CheckReachDestinationForExit();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            CallThanksSequence();
        }
    }

    public void DisableAllBools()
    {
        animator.SetBool(walkString, false);
        animator.SetBool(deadString, false);
    }

    public void GoToDoctorPosition()
    {
        DisableAllBools();
        agent.SetDestination(patientTargetTransform.position);
        //agent.isStopped = false;
        startCheckDistance = true;
        state = PatienteState.WALK;
        reachedDoctor = false;
        StartCoroutine("WaitFramesDoctor");
    }

    public IEnumerator WaitFramesDoctor()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        doctorDestinationSet = true;
    }

    public void GoToExitPosition()
    {
        patientServed = true;
        startCheckDistance = true;
        state = PatienteState.WALK;
        agent.SetDestination(patientExitTransform.position);
        reachedExit = false;
        StartCoroutine("WaitFramesExit");
    }

    public IEnumerator WaitFramesExit()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        exitDestinationSet = true;
    }

    private IEnumerator WaitAndLeave()
    {
        if (rotationBehaviour != null)
        {
            rotationBehaviour.StartRotatingBody();
        }
        if (objectActivator != null)
        {
            objectActivator.ActivateObject();
        }
        if (pissbehaviour != null)
        {
            pissbehaviour.StartParticleSystem();
        }
        GameManager.Instance.audioManager.PlayAudio(actionSound);
        GameManager.Instance.uiManager.ShowBubbleText(thanksString);
        yield return new WaitForSeconds(2f);
        GoToExitPosition();
    }


    public void CallThanksSequence()
    {
        StopCoroutine("WaitAndLeave");
        StartCoroutine("WaitAndLeave");
    }
    public void OnPatientProblemSolved()
    {
        GameManager.Instance.uiManager.ShowProblemSolvedBubble(thanksString, GoToExitPosition);
    }

    public void CheckReachDestinationForDoctor()
    {
        if (startCheckDistance)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                state = PatienteState.IDLE;
                DisableAllBools();
                GameManager.Instance.uiManager.ShowBubbleText(story);
                reachedDoctor = true;
                startCheckDistance = false;
                Debug.Log("ENTERED DOCTOR AREA " + gameObject.name);
                GameManager.Instance.audioManager.PlayAudio(speakClip);
            }
        }
        /*
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.1f && !reachedDoctor)
            {
                state = PatienteState.IDLE;
                DisableAllBools();
                GameManager.Instance.uiManager.ShowBubbleText(story);
                reachedDoctor = true;
                Debug.Log("ENTERED DOCTOR AREA " + gameObject.name);

            }

        }*/
    }

    public void CheckReachDestinationForExit()
    {
        if (startCheckDistance)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                state = PatienteState.IDLE;
                DisableAllBools();
                //GameManager.Instance.uiManager.ShowBubbleText(story);
                reachedExit = true;
                startCheckDistance = false;
                Debug.Log("ARRIVED EXIT AREA " + gameObject.name);
                OnPatientExit?.Invoke();
            }
        }
    }
}

public enum PatienteState
{
    WALK,
    IDLE,
    DEAD,
    DEFAULT
}

