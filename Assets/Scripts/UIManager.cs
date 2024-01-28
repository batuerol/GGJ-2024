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

    [Header("Item Name Text")]
    public TextMeshProUGUI itemText;

    [Header("TEXT BG")]
    public GameObject textBG;

    [Header("Wasted Canvas")]
    public GameObject wastedBG;
    public GameObject wastedYaziBG;
    public AudioClip wastedSound;

    private Action OnProblemSolved;

    private void Start()
    {
        patientStoryBubble.transform.localScale = Vector3.zero;
        wastedBG.SetActive(false);
        wastedYaziBG.SetActive(false);
    }
    public void ShowBubbleText(string story)
    {
        patientStoryText.text = story;
        patientStoryBubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        Invoke("HideBubbleText", 5f);
    }

    public void HideBubbleText()
    {
        patientStoryBubble.transform.DOScale(Vector3.zero, 0.3f);
    }

    public void HideTextWithAction()
    {
        patientStoryBubble.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => OnProblemSolved?.Invoke());
    }

    public void ShowProblemSolvedBubble(string thanksString, Action problemSolvedAction)
    {
        OnProblemSolved = problemSolvedAction;
        patientStoryText.text = thanksString;
        patientStoryBubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        Invoke("HideTextWithAction", 2f);
    }

    public void SetItemText(string itemName)
    {
        textBG.gameObject.SetActive(true);
        itemText.gameObject.SetActive(true);
        itemText.text = itemName;
    }

    public void DisableItemText()
    {
        textBG.gameObject.SetActive(false);
        itemText.gameObject.SetActive(false);
    }

    public void OnDead()
    {
        StartCoroutine("StartWastedSequence");
        //AudioManager
    }

    public IEnumerator StartWastedSequence()
    {
        GameManager.Instance.audioManager.PlayAudio(wastedSound);
        wastedBG.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        wastedYaziBG.SetActive(true);

    }
}
