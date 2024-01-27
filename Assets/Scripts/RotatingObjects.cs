using UnityEngine;
using DG.Tweening;

public class RotateObjects : MonoBehaviour
{
    public GameObject head;
    public GameObject body;

    public float rotationDuration = 2.0f; // Duration for one complete rotation

    void Start()
    {
        head.transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    public void StartRotatingBody()
    {
        // Rotate around X-axis indefinitely
        body.transform.DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }

    public void StopRotatingBody()
    {
        head.transform.DOKill();
        body.transform.DOKill();
    }
}
