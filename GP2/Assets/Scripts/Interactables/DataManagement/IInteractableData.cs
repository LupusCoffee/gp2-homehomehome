using UnityEngine;

public enum InteractionType : byte {
    STATIC_OBSTACLE = 0,
    MOVING_OBSTACLE = 1,
    TRANSITION = 2,
    TRANSITION_TO_AREA = 3,
    CUTSCENE = 4,
    VERBAL = 5,
    ORTHOGRAPHIC = 6,
    ITEM = 7,
    SPELL_ATTRIBUTE = 8
}

public interface IInteractableData {
    public IInteractableData GetInteractableData();

    public int GetID();

    public bool SetID(int interactableID);
    //public bool ChangeData();

    public InteractionType GetInteractionType();

    public void SetInteractionType(InteractionType type);

    public bool ChangeData(IInteractableData interactableData);
}
