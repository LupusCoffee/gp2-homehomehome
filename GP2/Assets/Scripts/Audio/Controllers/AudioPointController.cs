using UnityEngine;

public class AudioPointController : MonoBehaviour
{
    [SerializeField] AK.Wwise.Event pointEvent;

    private void Start()
    {
        pointEvent.Post(gameObject);
    }
}
