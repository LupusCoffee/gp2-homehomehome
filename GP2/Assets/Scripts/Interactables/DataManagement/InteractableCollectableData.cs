using UnityEngine;

public class InteractableCollectableData : InteractableData {
    [SerializeField] protected string collectableName = "Default_Collectable";

    public InteractableCollectableData() : base(InteractionType.ITEM) {}

    public InteractableCollectableData(int interactableID) : base(interactableID, InteractionType.ITEM) {}

    public InteractableCollectableData(InteractionType type) : base(InteractionType.ITEM) {}

    public InteractableCollectableData(string collectableName) : base (InteractionType.ITEM) { 
        this.collectableName = collectableName; 
    }

    public InteractableCollectableData(int interactableID, string collectableName) : base(interactableID, InteractionType.ITEM) { 
        this.collectableName = collectableName; 
    }

    public InteractableCollectableData(InteractionType type, string collectableName) : base(InteractionType.ITEM) {
        this.collectableName = collectableName;
    }

    public InteractableCollectableData(int interactableID, InteractionType type) : base(interactableID, InteractionType.ITEM) {}

    public InteractableCollectableData(int interactableID, InteractionType type, string collectableName) : base(interactableID, 
            InteractionType.ITEM) {
        this.collectableName = collectableName;
    }

    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) { 
        //this.type = InteractionType.ITEM;
    }

    public string GetCollectableName() { return this.collectableName; }

    public void SetCollectableName(string collectableName) { this.collectableName = collectableName; }


    public override bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        //this.type = InteractionType.ITEM;
        return true;
    }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableData.GetID() < 0 && interactableData.GetType() != typeof(InteractableCollectableData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = InteractionType.ITEM;
        this.collectableName = ((InteractableCollectableData)interactableData).GetCollectableName();
        return true;
    }

    public virtual bool ChangeData(int interactableID, InteractionType type, string collectableName) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = InteractionType.ITEM;
        this.collectableName = collectableName;
        return true;
    }
}
