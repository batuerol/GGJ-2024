using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectActivator : MonoBehaviour
{
    public GameObject activatedObject;

    Vector3 defaultScale;
    // Start is called before the first frame update
    void Start()
    {
        activatedObject.SetActive(false);
        defaultScale = activatedObject.transform.localScale;
    }

    public void ActivateObject()
    {
        activatedObject.transform.localScale = Vector3.zero;
        activatedObject.SetActive(true);
        activatedObject.transform.DOScale(defaultScale, 0.3f).SetEase(Ease.InCubic);
    }
}
