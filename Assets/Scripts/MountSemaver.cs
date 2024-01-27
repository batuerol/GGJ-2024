using UnityEngine;

public class SemaverCollision : MonoBehaviour
{
    public GameObject draggableSemaver;
    public GameObject semaveronarm;

    private void Start()
    {
        // Ensure semaverlik is initially invisible if needed
        semaveronarm.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the draggable semaver
        if (collision.gameObject == draggableSemaver)
        {
            print("collide eyledi");
            // make visible invisible
            semaveronarm.SetActive(true);
            draggableSemaver.SetActive(false);
        }
    }
}
