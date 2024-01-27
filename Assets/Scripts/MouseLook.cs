using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera camera = null;
    public float sensitivity = 2.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;
    private Quaternion startRotation;
    public delegate void OnHitCallback(GameObject gameObject);
    public OnHitCallback onHitHandler;

    void Start()
    {
        if (camera == null)
            camera = Camera.main;

        startRotation = camera.transform.localRotation;
    }

    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        Quaternion quatX = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion quatY = Quaternion.AngleAxis(rotationY, Vector3.left);

        camera.transform.localRotation = startRotation * quatX * quatY;

        int centerX = Screen.width / 2;
        int centerY = Screen.height / 2;

        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5000f, LayerMask.NameToLayer("GGJ_Selectable")))
        {
            if (hit.transform.gameObject.GetComponent<SelectableItem>() != null)
            {
                Debug.Log("START OTLINE AT: " + hit.transform.name);
                hit.transform.gameObject.GetComponent<SelectableItem>().SetSelected();
            }
            if (Input.GetMouseButtonDown(0))
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
