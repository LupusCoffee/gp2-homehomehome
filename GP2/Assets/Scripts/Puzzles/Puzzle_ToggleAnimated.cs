using UnityEngine;

public class Puzzle_ToggleAnimated : SpellActivatable
{
    public bool isActivated = false;

    [Header("Optional Animator not on this object, otherwise it defaults to self")]
    [SerializeField] Animator animator;

    private void Start()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }

    public override void ActivateBySpell()
    {
        Debug.Log("Spell Activated");
        isActivated = !isActivated;
        animator.SetBool("isActivated", isActivated);
    }
}
