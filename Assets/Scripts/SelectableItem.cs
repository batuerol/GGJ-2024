using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableItem : MonoBehaviour
{
    public Outline outlineReference;

    public string itemName;
    public ItemType itemType;

    public bool selected = false;


    public void SetSelected()
    {
        selected = true;
        //GetComponent<Outline>().enabled = true;
        outlineReference.OutlineWidth = 10f;
    }

    public void DeselectItem()
    {
        selected = false;
        outlineReference.OutlineWidth = 0f;
    }

    public void ThrowItemToPatient()
    {

        //item'i hastaya firlat!
    }

}
