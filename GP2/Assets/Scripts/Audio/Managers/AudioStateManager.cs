using UnityEngine;

public class AudioStateManager : MonoBehaviour
{
    private void Start()
    {
        DefaultStates();
    }

    public void DefaultStates()
    {
        AkSoundEngine.SetState("AreaStates", "part1");
    }
}
