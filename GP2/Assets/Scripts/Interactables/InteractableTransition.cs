using UnityEngine;

public class InteractableTransition : Interactable {
    public InteractableTransition() : base(new InteractableTransitionData()) {}

    public InteractableTransition(InteractableTransitionData data) : base(data) {}

    public InteractableTransition(int interactableID) : base(new InteractableTransitionData(interactableID)) {}

    public InteractableTransition(string transitionName) : base(new InteractableTransitionData(transitionName)) {}

    public InteractableTransition(InteractableTransitionData data, InteractionState state) : base(data, state) {}

    protected override void Start() {
        base.Start();
        //this.type = InteractionType.TRANSITION_TO_AREA;
    }

    void Update() {

    }

    public override IInteractableData Interact() {
        return this.data;
    }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.TRANSITION_TO_AREA);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableTransition, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", TransitionName=" + 
            ((InteractableTransitionData)this.data).GetTransitionName() + "]");
    }
}
