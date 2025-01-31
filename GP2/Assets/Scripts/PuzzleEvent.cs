using UnityEngine;

public class PuzzleEvent : MonoBehaviour
{
    public delegate void OnPress(GameObject caller);
    public event OnPress OnButtonPressed;
    public event OnPress OnButtonReleased;

    protected void TriggerOnButtonPressed()
    {
        OnButtonPressed?.Invoke(gameObject);
    }

    protected void TriggerOnButtonReleased()
    {
        OnButtonReleased?.Invoke(gameObject);
    }

    private void OnDestroy()
    {
        OnButtonPressed = null;
        OnButtonReleased = null;
    }
}
