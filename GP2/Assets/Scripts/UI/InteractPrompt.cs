using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    public static InteractPrompt Instance;

    [Header("Components")]
    public CanvasGroup canvasGroup;
    public Image promptImage;
    
    private float _desiredAlpha;
    private float _currentAlpha;
    
    /// <summary>
    /// Singelton and sets the canvas alpha to 0
    /// </summary>
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        _currentAlpha = 0;
        _desiredAlpha = 0;
    }
    
    /// <summary>
    /// Fades the prompt in and out depending on the desiredAplha
    /// </summary>
    void Update() {
        _currentAlpha = Mathf.MoveTowards(_currentAlpha, _desiredAlpha, 2.0f * Time.deltaTime);
        canvasGroup.alpha = _currentAlpha;
    }

    /// <summary>
    /// Sets the sprite to the item sprite and then fades the prompt in
    /// </summary>
    /// <param name="itemSprite"></param>
    public void ShowInteractPrompt(Sprite itemSprite) {
        promptImage.sprite = itemSprite;
        _desiredAlpha = 1;
    }

    /// <summary>
    /// Fades the prompt out
    /// </summary>
    public void HideInteractPrompt() {
        _desiredAlpha = 0;
    }
}
