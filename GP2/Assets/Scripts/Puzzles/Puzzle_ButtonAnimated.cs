using UnityEngine;

public class Puzzle_ButtonAnimated : MonoBehaviour
{
    Animator animator;
    public bool isActivated = false;

    [SerializeField] PuzzleEvent activateButton;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (activateButton != null)
        {
            activateButton.OnButtonPressed += ActivateSelf;
            activateButton.OnButtonReleased += DeactivateSelf;
        }
    }

    public void ActivateSelf(GameObject obj)
    {
        isActivated = true;
        animator.SetBool("isActivated", isActivated);
    }

    public void DeactivateSelf(GameObject obj)
    {
        isActivated = false;
        animator.SetBool("isActivated", isActivated);
    }

    private void OnDestroy()
    {
        if (activateButton != null)
        {
            activateButton.OnButtonPressed -= ActivateSelf;
            activateButton.OnButtonReleased -= DeactivateSelf;
        }
    }
}
