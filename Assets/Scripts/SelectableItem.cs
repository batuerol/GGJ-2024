using UnityEngine;
using DG.Tweening;

public class SelectableItem : MonoBehaviour
{
    public Outline outlineReference;

    public string itemName;
    public ItemType itemType;

    public bool is_picked_up = false;

    public void SetSelected()
    {
        //GetComponent<Outline>().enabled = true;
        outlineReference.OutlineWidth = 10f;
        GameManager.Instance.currentSelectable = this;
    }

    public void DeselectItem()
    {
        outlineReference.OutlineWidth = 0f;
        GameManager.Instance.currentSelectable = null;
    }

    public void PickupItem()
    {
        SetSelected();
        is_picked_up = true;
        gameObject.transform.SetParent(GameManager.Instance.fpsController.pickedObjectTransform);
        transform.DOLocalMove(GameManager.Instance.fpsController.pickedObjectTransform.localPosition, 0.5f);
        gameObject.transform.localPosition = new Vector3(0.05f, 0f, 8f);
        gameObject.transform.localEulerAngles = Vector3.zero;
    }

    public void ThrowItemToPatient()
    {
        if (is_picked_up)
        {
            transform.DOJump(GameManager.Instance.currentPatient.transform.position, 2, 1, 1);
        }
    }

}
