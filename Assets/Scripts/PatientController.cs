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

    public PatienteState state;

    private void Start()
    {
        DisableAllBools();
        state = PatienteState.DEFAULT;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GoToTargetPosition();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GoToExitTransform();
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

        CheckReachDestination();
    }

    public void DisableAllBools()
    {
        animator.SetBool(walkString, false);
        animator.SetBool(deadString, false);
    }

    public void GoToTargetPosition()
    {
        agent.SetDestination(patientTargetTransform.position);
        state = PatienteState.WALK;
    }
    public void GoToExitTransform()
    {
        agent.SetDestination(patientExitTransform.position);
        state = PatienteState.WALK;
    }

    public void CheckReachDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    DisableAllBools();
                    GameManager.Instance.uiManager.ShowBubbleText(story);
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
