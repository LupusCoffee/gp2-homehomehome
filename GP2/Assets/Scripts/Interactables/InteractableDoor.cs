using UnityEngine;

public class InteractableDoor : Interactable {
    public InteractableDoor() : base(new InteractableDoorData()) {}

    public InteractableDoor(InteractableDoorData data) : base(data) {}

    public InteractableDoor(int interactableID) : base(new InteractableDoorData(interactableID)) {}

    public InteractableDoor(string doorName) : base(new InteractableDoorData(doorName)) {}

    public InteractableDoor(InteractableDoorData data, InteractionState state) : base(data, state) {}

    protected override void Start() {
        base.Start();
        //this.type = InteractionType.TRANSITION;
    }

    void Update() {

    }

    public override IInteractableData Interact() {
        return this.data;
    }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.TRANSITION);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableDoor, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", DoorName=" + ((InteractableDoorData)this.data).GetDoorName() + "]");
    }
}
