using UnityEngine;

public class DeathController : MonoBehaviour
{
    private GameObject currentCheckpoint;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Checkpoint"))
            currentCheckpoint = col.gameObject;

        if (col.gameObject.CompareTag("DeathTrigger"))
            gameObject.transform.position = currentCheckpoint.transform.position;
    }
}
