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

    [Header("Patient Story")]
    public string story;
    public string thanksString;

    public PatienteState state;

    public bool reachedDoctor = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GoToDoctorPosition();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GoToExitPosition();
        }

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

        if (!reachedDoctor)
        {
            //CheckReachDestinationForDoctor();
        }
        /*
        else
        {
            CheckReachDestinationForExit();
        }
        */
    }

    public void DisableAllBools()
    {
        animator.SetBool(walkString, false);
        animator.SetBool(deadString, false);
    }

    public void GoToDoctorPosition()
    {
        DisableAllBools();

        state = PatienteState.WALK;
        agent.SetDestination(patientTargetTransform.position);
        reachedDoctor = false;
    }
    public void GoToExitPosition()
    {
        agent.SetDestination(patientExitTransform.position);
        state = PatienteState.WALK;
    }

    public void OnPatientProblemSolved()
    {
        GameManager.Instance.uiManager.ShowProblemSolvedBubble(thanksString, GoToExitPosition);
    }

    public void CheckReachDestinationForDoctor()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (/*!agent.hasPath || agent.velocity.sqrMagnitude == 0.1f && */!reachedDoctor)
                {
                    state = PatienteState.IDLE;
                    DisableAllBools();
                    GameManager.Instance.uiManager.ShowBubbleText(story);
                    reachedDoctor = true;
                    Debug.Log("ENTERED DOCTOR AREA");

                }
            }
        }
    }

    public void CheckReachDestinationForExit()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0.1f)
                {
                    // Done
                    //agent.path = null;
                    DisableAllBools();
                    GameManager.Instance.CallNextPatient();
                }
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

