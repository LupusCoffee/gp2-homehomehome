//Created by Mohammed (the sex god)

using UnityEngine;

public class AudioZoneController : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event enterEvent, exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) EnterZone(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) ExitZone(other.gameObject);
    }

    protected virtual void EnterZone(GameObject player)
    {
        enterEvent.Post(this.gameObject);
    }
    protected virtual void ExitZone(GameObject player)
    {
        exitEvent.Post(this.gameObject);
    }
}
