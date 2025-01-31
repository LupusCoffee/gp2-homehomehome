using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp_LoadNext : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TransitionAnimator.Instance.LoadGame(1);
        }
    }


}
