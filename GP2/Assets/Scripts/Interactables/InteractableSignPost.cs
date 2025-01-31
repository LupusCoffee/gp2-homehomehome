using UnityEngine;

public class InteractableSignPost : Interactable {
    public InteractableSignPost() : base(new InteractableSignPostData()) {}

    public InteractableSignPost(InteractableSignPostData data) : base(data) {}

    public InteractableSignPost(int interactableID) : base(new InteractableSignPostData(interactableID)) {}

    public InteractableSignPost(string signText) : base(new InteractableSignPostData(signText)) {}

    public InteractableSignPost(InteractableSignPostData data, InteractionState state) : base(data, state) {}

    protected override void Start() {
        base.Start();
        //this.type = InteractionType.ORTHOGRAPHIC;
    }

    void Update() {

    }

    public override IInteractableData Interact() {
        return this.data;
    }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.ORTHOGRAPHIC);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableSignPost, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", SignText=" + ((InteractableSignPostData)this.data).GetSignText() + "]");
    }
}
