using UnityEngine;
using DG.Tweening;

public class RotatingObjects : MonoBehaviour
{
    public GameObject head;
    public GameObject body;

    public float rotationDuration = 2.0f; // Duration for one complete rotation

    void Start()
    {
        head.transform.DOLocalRotate(new Vector3(-360f,0f, 0f), 0.3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    public void StartRotatingBody()
    {
        // Rotate around X-axis indefinitely
        body.transform.DOLocalRotate(new Vector3(0f, 360f, 0f), 0.3f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    public void StopRotatingBody()
    {
        head.transform.DOKill();
        body.transform.DOKill();
    }
}
