using UnityEngine;

public class InteractableDoorData : InteractableData {
    [SerializeField] private string doorName = "Default_Door";

    public InteractableDoorData() : base(InteractionType.TRANSITION) { }

    public InteractableDoorData(int interactableID) : base(interactableID, InteractionType.TRANSITION) { }

    public InteractableDoorData(InteractionType type) : base(InteractionType.TRANSITION) { }

    public InteractableDoorData(string doorName) : base(InteractionType.TRANSITION) { this.doorName = doorName; }

    public InteractableDoorData(int interactableID, string doorName) : base(interactableID, InteractionType.ITEM) {
        this.doorName = doorName;
    }

    public InteractableDoorData(InteractionType type, string doorName) : base(InteractionType.TRANSITION) {
        this.doorName = doorName;
    }

    public InteractableDoorData(int interactableID, InteractionType type) : base(interactableID, InteractionType.TRANSITION) { }

    public InteractableDoorData(int interactableID, InteractionType type, string doorName) : 
            base(interactableID, InteractionType.TRANSITION) {
        this.doorName = doorName;
    }


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.TRANSITION;
    }

    public string GetDoorName() { return this.doorName; }

    public void SetDoorName(string doorName) { this.doorName = doorName; }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableID < 0 && interactableData.GetType() != typeof(InteractableDoorData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = type;
        this.doorName = ((InteractableDoorData)interactableData).GetDoorName();
        return true;
    }

    public override bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        //this.doorName = "Default_door_name";
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string doorName) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = InteractionType.TRANSITION;
        this.doorName = doorName;
        return true;
    }
}
