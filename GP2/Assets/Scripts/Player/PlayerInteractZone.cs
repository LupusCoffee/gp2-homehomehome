using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractZone : MonoBehaviour {
    [SerializeField] List<GameObject> interactablesInRange = new List<GameObject>();

    private void Update() {
        /*foreach (GameObject gameObject in this.interactablesInRange) {
            Interactable interactable = gameObject.GetComponent<Interactable>();

            if (gameObject.GetComponent<Collider>().enabled == false) {
                this.interactablesInRange.Remove(gameObject);
                //Needs to be updated with different InteractionTypes!!
                if (interactable.GetInteractionType() == InteractionType.ITEM) {
                    InteractPrompt.Instance.HideInteractPrompt();
                }

                //Shouldn't be done here in PlayerInteractZone?
                //Destroy(gameObject);
            }
        }*/
        if (this.interactablesInRange.Count > 0) {
            GameObject gameObject = this.interactablesInRange[0];
            if (gameObject.GetComponent<Collider>().enabled == false) {
                
                //Needs to be updated with different InteractionTypes!!
                if (gameObject.GetComponent<Interactable>().GetInteractionType() == InteractionType.ITEM) {
                    InteractPrompt.Instance.HideInteractPrompt();
                }
                this.interactablesInRange.RemoveAt(0);
                //Shouldn't be done here in PlayerInteractZone?
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent(out Interactable interactable)) {
            interactablesInRange.Add(other.gameObject);
            if (interactable.GetInteractionType() == InteractionType.ITEM) {
                InteractPrompt.Instance.ShowInteractPrompt(InteractPrompt.Instance.promptImage.sprite);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.TryGetComponent(out Interactable interactable)) {
            interactablesInRange.Remove(other.gameObject);

            if (interactable.GetInteractionType() == InteractionType.ITEM) {
                InteractPrompt.Instance.HideInteractPrompt();
            }
        }
    }

    private Interactable GetFirstActiveInteractable() {
        if (interactablesInRange.Count > 0)
            return interactablesInRange[0].GetComponent<Interactable>();
        return null;
    }

    public Interactable GetInteractable() { return GetFirstActiveInteractable(); }

    public IInteractableData GetInteractableData() { return GetFirstActiveInteractable().GetInteractableData(); }
}
