using System.Collections;
using System.Collections.Generic;
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
        transform.DOMove(Camera.main.transform.position, 0.5f);
    }

    public void ThrowItemToPatient()
    {
        //item'i hastaya firlat!        

        if (is_picked_up)
        {
            transform.DOJump(GameManager.Instance.currentPatient.transform.position,
                2, 1, 1);
        }
    }

}
