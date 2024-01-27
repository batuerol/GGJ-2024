using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

        if (GameManager.Instance.currentSelectable != null &&
            GameManager.Instance.currentSelectable.is_picked_up)
        {
            if (Input.GetMouseButtonDown(0)) {
                GameManager.Instance.currentSelectable.ThrowItemToPatient();
                GameManager.Instance.currentSelectable.is_picked_up = false;
                GameManager.Instance.currentSelectable = null;
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5000f))
            {
                if (hit.transform.gameObject.GetComponent<SelectableItem>() != null)
                {
                    Debug.Log("START OTLINE AT: " + hit.transform.name);
                    hit.transform.gameObject.GetComponent<SelectableItem>().SetSelected();

                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        //onHitHandler(hit.transform.gameObject);
                        hit.transform.gameObject.GetComponent<SelectableItem>().PickupItem();                        
                    }
                }
                else
                {
                    GameManager.Instance.DeselectAllItems();
                }
            }
            else
            {
                GameManager.Instance.DeselectAllItems();
            }
        }
    }
}
