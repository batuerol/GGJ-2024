using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Patient Story Items")]
    public GameObject patientStoryBubble;
    public TextMeshProUGUI patientStoryText;
    // Start is called before the first frame update

    private Action OnProblemSolved;

    private void Start()
    {
        patientStoryBubble.transform.localScale = Vector3.zero;
    }
    public void ShowBubbleText(string story)
    {
        Debug.Log("SHOWING BUBBLE TEXT");
        patientStoryText.text = story;
        patientStoryBubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        Invoke("HideBubbleText", 2f);
    }

    public void HideBubbleText()
    {
        patientStoryBubble.transform.DOScale(Vector3.zero, 0.3f);
    }

    public void HideTextWithAction()
    {
        patientStoryBubble.transform.DOScale(Vector3.zero, 0.3f).OnComplete(()=> OnProblemSolved?.Invoke());
    }

    public void ShowProblemSolvedBubble(string thanksString, Action problemSolvedAction)
    {
        OnProblemSolved = problemSolvedAction;
        patientStoryText.text = thanksString;
        patientStoryBubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        Invoke("HideTextWithAction", 2f);
    }
}
