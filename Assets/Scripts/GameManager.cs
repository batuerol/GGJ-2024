using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager uiManager;
    public DoorAnimation doorAnimation;
    public MouseLook mouseLook;

    public SelectableItem currentSelectable;

    public List<SelectableItem> itemList;

    int currentPatientIndex = 0;

    public List<PatientController> patientList;
    public PatientController currentPatient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        CallNextPatient();
    }
    // Start is called before the first frame update
    public void CallNextPatient()
    {
        if (currentPatientIndex <= patientList.Count)
        {
            currentPatient = patientList[currentPatientIndex];
            currentPatient.gameObject.SetActive(true);
            currentPatient.GoToDoctorPosition();
        }

        /*
        currentPatient = patientList[currentPatientIndex];
        currentPatient.gameObject.SetActive(true);
        currentPatient.GoToTargetPosition();
        */
    }

    public void DeselectAllItems()
    {
        foreach (SelectableItem item in itemList) {

            item.DeselectItem();
        }
    }
}
