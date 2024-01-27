using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UIManager uiManager;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CallNextPatient()
    {
        currentPatient = patientList[currentPatientIndex];
        currentPatient.gameObject.SetActive(true);
        currentPatient.GoToTargetPosition();
    }
}
