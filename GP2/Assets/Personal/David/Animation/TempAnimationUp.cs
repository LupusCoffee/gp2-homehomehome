using UnityEngine;

public class TempAnimationUp : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string animationName = "Temp_animation Up"; // Name of the animation to play

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers the animation
        {
            if (animator != null)
            {
                animator.SetTrigger("Up");
            }
            else
            {
                Debug.LogWarning("Animator component not assigned!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers the animation
        {
            if (animator != null)
            {
                animator.SetTrigger("Down");
            }
            else
            {
                Debug.LogWarning("Animator component not assigned!");
            }
        }
    }
}
