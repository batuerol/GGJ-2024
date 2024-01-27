using UnityEngine;

public class DragByMouseClick : MonoBehaviour
{
    public GameObject body;
    private bool isDragging = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // degistir baska camera ise
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
                // Check if the hit object is this object
                if (hit.collider.gameObject == gameObject)
                    isDragging = !isDragging;
        }

        // Move the object if dragging is true
        if (isDragging)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.WorldToScreenPoint(gameObject.transform.position).z);
            Vector3 objPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = objPosition;
        }
    }
}
