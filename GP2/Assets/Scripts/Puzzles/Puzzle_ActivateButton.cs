using System.Collections;
using UnityEngine;
using static CompactMath;

// Button that only has an activation event, no deactivation event
public class Puzzle_ActivateButton : Interactable
{
    public delegate void OnPress(GameObject obj);
    public event OnPress OnButtonPressed;
    bool hasBeenPressed = false;

    [SerializeField] GameObject buttonCrystal;

    public bool isPressable;
 
    protected override void Start()
    {
        buttonCrystal.SetActive(false);
        buttonCrystal.transform.position = transform.position;
    }

    private void Update()
    {
        if (hasBeenPressed) {
            buttonCrystal.transform.Rotate(Vector3.up, 1);
        }
    }

    public override IInteractableData Interact()
    {
        if (hasBeenPressed || !isPressable) return null;

        hasBeenPressed = true;
        OnButtonPressed?.Invoke(gameObject);

        buttonCrystal.SetActive(true);
        LeanTween.move(buttonCrystal, transform.position + new Vector3(0, 1.5f, 0), 1.5f).setEaseInOutCubic();

        return null;
    }

    public IEnumerator ResetButton()
    {
        yield return new WaitForSeconds(1F);
        hasBeenPressed = false;
        LeanTween.move(buttonCrystal, transform.position, 0.5f).setEaseInOutCubic();
        LeanTween.rotate(buttonCrystal, Vector3.zero, 0.5f).setEaseInOutCubic();

        yield return new WaitForSeconds(0.5F);
        buttonCrystal.SetActive(false);
    }

    private void OnDestroy()
    {
        OnButtonPressed = null;
    }

}
