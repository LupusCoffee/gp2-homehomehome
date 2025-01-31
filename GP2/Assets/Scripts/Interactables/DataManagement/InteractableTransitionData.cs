using UnityEngine;

public class InteractableTransitionData : InteractableData {
    [SerializeField] private string transitionName = "Default_Transition";

    public InteractableTransitionData() : base(InteractionType.TRANSITION_TO_AREA) {}

    public InteractableTransitionData(int interactableID) : base(interactableID, InteractionType.TRANSITION_TO_AREA) {}

    public InteractableTransitionData(InteractionType type) : base(InteractionType.TRANSITION_TO_AREA) {}

    public InteractableTransitionData(string transitionName) : base(InteractionType.TRANSITION_TO_AREA) { 
        this.transitionName = transitionName; 
    }

    public InteractableTransitionData(int interactableID, string transitionName) : 
            base(interactableID, InteractionType.TRANSITION_TO_AREA) {
        this.transitionName = transitionName;
    }

    public InteractableTransitionData(InteractionType type, string transitionName) : base(InteractionType.TRANSITION_TO_AREA) {
        this.transitionName = transitionName;
    }

    public InteractableTransitionData(int interactableID, InteractionType type) : 
            base(interactableID, InteractionType.TRANSITION_TO_AREA) {}


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.TRANSITION_TO_AREA;
    }

    public string GetTransitionName() { return this.transitionName; }

    public void SetTransitionName(string transitionName) { this.transitionName = transitionName; }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableID < 0 && interactableData.GetType() != typeof(InteractableTransitionData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = type;
        this.transitionName = ((InteractableTransitionData)interactableData).GetTransitionName();
        return true;
    }
    public override bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        //this.transitionName = "Default_Transition";
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string transitionName) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = InteractionType.TRANSITION_TO_AREA;
        this.transitionName = transitionName;
        return true;
    }
}
