using UnityEngine;

public class SpellActivatable : MonoBehaviour
{
    protected bool _isEnabled = false;
    protected bool _canBeDisabled = true;

    public virtual void ActivateBySpell()
    {
        Debug.Log("Spell Activated");
    }
}
