using UnityEngine;

public class InteractableArtifact : InteractableCollectable {
    public InteractableArtifact() : base(new InteractableArtifactData()) {}

    public InteractableArtifact(InteractableArtifactData data) : base(data) {}

    public InteractableArtifact(int interactableID) : base(new InteractableArtifactData(interactableID)) {}

    public InteractableArtifact(string collectableName) : base(new InteractableArtifactData(collectableName)) {}

    public InteractableArtifact(InteractableArtifactData data, InteractionState state) : base(data, state) {}


    protected override void Start() {
        base.Start();
        //this.type = InteractionType.ITEM;
        Debug.Log($"ID {GetID()}");
    }

    void Update() {

    }

    public override IInteractableData Interact() { return this.data; }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.ITEM);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableArtifact, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", ArtifactName=" + ((InteractableArtifactData)this.data).GetCollectableName() +
            ", LoreText=" + ((InteractableArtifactData)this.data).GetLoreText() + "]");
    }
}
