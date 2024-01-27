using UnityEngine;

public class DragByMouseClick : MonoBehaviour
{
    public PatientController patientController;

    private bool isDragging = false;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // degistir baska camera ise
    }

    void Update()
    {
        if (patientController.patientServed)
        {
            return;
        }
        int centerX = Screen.width / 2;
        int centerY = Screen.height / 2;

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            //Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5000f))
                // Check if the hit object is this object
                if (hit.collider.gameObject == gameObject)
                    isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                patientController.CallThanksSequence();
            }
        }

        // Move the object if dragging is true
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(centerX, centerY, Camera.main.WorldToScreenPoint(gameObject.transform.position).z);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }
}
