using System.Runtime.Serialization;
using UnityEngine;

public class InteractableData : IInteractableData {
    [SerializeField] protected int interactableID = -1;
    [SerializeField] protected InteractionType type = InteractionType.STATIC_OBSTACLE;

    public InteractableData() {
        this.interactableID = -1;
        this.type = InteractionType.STATIC_OBSTACLE;
    }

    public InteractableData(int interactableID) {
        this.interactableID = interactableID;
        this.type = InteractionType.STATIC_OBSTACLE;
    }

    public InteractableData(InteractionType type) {
        this.interactableID = -1;
        this.type = type;
    }

    public InteractableData(int interactableID, InteractionType type) {
        if (interactableID < 0)
            this.interactableID = interactableID;
        else
            this.interactableID = -1;
        this.type = type;
    }

    public virtual IInteractableData GetInteractableData() { return this; }

    public int GetID() { return this.interactableID; }

    public InteractionType GetInteractionType() { return this.type; }

    public virtual bool SetID(int interactableID) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        return true;
    }

    public virtual void SetInteractionType(InteractionType type) { this.type = type; }

    public virtual bool ChangeData(IInteractableData interactableData) {
        if (interactableData.GetID() < 0) { return false; }
        this.interactableID = interactableData.GetID();
        this.type = interactableData.GetInteractionType();
        return true;
    }

    public virtual bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        this.type = type;
        return true;
    }
}
