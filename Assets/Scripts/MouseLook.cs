using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensitivity = 2.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Quaternion startRotation;
    public delegate void OnHitCallback(GameObject gameObject);
    public OnHitCallback onHitHandler;

    void Start()
    {

        startRotation = GetComponent<Camera>().transform.localRotation;
    }

    void Update()
    {
        /*
        Cursor.visible = false;

        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        Quaternion quatX = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion quatY = Quaternion.AngleAxis(rotationY, Vector3.left);

        Camera.main.transform.localRotation = startRotation * quatX * quatY;

        int centerX = Screen.width / 2;
        int centerY = Screen.height / 2;
        */

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5000f, LayerMask.NameToLayer("GGJ_Selectable")))
        {
            if (hit.transform.gameObject.GetComponent<SelectableItem>() != null)
            {
                Debug.Log("START OTLINE AT: " + hit.transform.name);
                hit.transform.gameObject.GetComponent<SelectableItem>().SetSelected();
            }
            else
            {
                GameManager.Instance.DeselectAllItems();
            }

            if (Input.GetMouseButtonDown(0) && hit.transform.gameObject.GetComponent<SelectableItem>() != null)
            {
                Debug.Log(hit.transform.gameObject.name);
                onHitHandler(hit.transform.gameObject);
            }
        }
        else
        {
            GameManager.Instance.DeselectAllItems();
        }
    }
}
