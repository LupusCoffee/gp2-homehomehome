using UnityEngine;

public enum InteractionState : byte {
    OFF = 0,
    ON = 1,
    ALT_STATE_1 = 2,
    ALT_STATE_2 = 3,
    ALT_STATE_3 = 4
}

public interface IInteractable {
    public IInteractableData Interact();

    public IInteractableData GetInteractableData();
    public bool ChangeState(InteractionState interactionState);
}
