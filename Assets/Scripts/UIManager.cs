using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Patient Story Items")]
    public GameObject patientStoryBubble;
    public TextMeshProUGUI patientStoryText;
    // Start is called before the first frame update
    private void Start()
    {
        patientStoryBubble.transform.localScale = Vector3.zero;
    }
    public void ShowBubbleText(string story)
    {
        patientStoryText.text = story;
        patientStoryBubble.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce);
        Invoke("HideBubbleText" , 2f);
    }

    public void HideBubbleText()
    {
        patientStoryBubble.transform.DOScale(Vector3.zero, 0.3f);
    }
}
