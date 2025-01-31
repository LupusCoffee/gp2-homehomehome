using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public abstract class InteractableCollectable : Interactable {
    public InteractableCollectable() : base(new InteractableCollectableData()) {}

    public InteractableCollectable(InteractableCollectableData data) : base(data) {}

    public InteractableCollectable(int interactableID) : base(new InteractableCollectableData(interactableID)) {}

    public InteractableCollectable(string collectableName) : base(new InteractableCollectableData(collectableName)) {}

    public InteractableCollectable(InteractableCollectableData data, InteractionState state) : base(data, state) {}

    protected override void Start() {
        base.Start();
        //this.type = InteractionType.ITEM;
		Debug.Log($"ID {GetID()}");
    }

    void Update() {

    }

    public override IInteractableData Interact() {
        return this.data;
    }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.ITEM);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableCollectable, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", CollectableName=" + 
            ((InteractableCollectableData)this.data).GetCollectableName() + "]");
    }
}
