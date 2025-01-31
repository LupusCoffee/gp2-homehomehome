using UnityEngine;

public class Puzzle_SpellButton : SpellActivatable
{
    public delegate void OnPress(GameObject obj);
    public event OnPress OnButtonPressed;

    public override void ActivateBySpell()
    {
        base.ActivateBySpell();

        OnButtonPressed?.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        OnButtonPressed = null;
    }
}
