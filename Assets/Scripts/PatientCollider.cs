using UnityEngine;

public class PatientCollider : MonoBehaviour
{
    public PatientController patientController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemCollider"))
        {
            Debug.Log("COLLIDING ITEM");
            if (other.GetComponent<SelectableItem>().itemType == patientController.requiredItemType)
            {
                Debug.Log("PATIENTE GRABBED ITEM");
                GameManager.Instance.currentSelectable = null;
                other.gameObject.SetActive(false);
                patientController.CallThanksSequence();
            }
        }
    }
}
