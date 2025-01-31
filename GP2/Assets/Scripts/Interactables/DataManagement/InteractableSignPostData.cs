using UnityEngine;

public class InteractableSignPostData : InteractableData {
    [SerializeField] private string signText = "Hi";

    public InteractableSignPostData() : base(InteractionType.ORTHOGRAPHIC) {}

    public InteractableSignPostData(int interactableID) : base(interactableID, InteractionType.ORTHOGRAPHIC) {}

    public InteractableSignPostData(InteractionType type) : base(InteractionType.ORTHOGRAPHIC) {}

    public InteractableSignPostData(string signTextName) : base(InteractionType.ORTHOGRAPHIC) { this.signText = signTextName; }

    public InteractableSignPostData(int interactableID, string signText) : base(interactableID, InteractionType.ORTHOGRAPHIC) {
        this.signText = signText;
    }

    public InteractableSignPostData(InteractionType type, string signText) : base(InteractionType.ORTHOGRAPHIC) {
        this.signText = signText;
    }

    public InteractableSignPostData(int interactableID, InteractionType type) : base(interactableID, InteractionType.ORTHOGRAPHIC) {}


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.ORTHOGRAPHIC;
    }

    public string GetSignText() { return this.signText; }

    public void SetSignText(string signText) { this.signText = signText; }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableID < 0 && interactableData.GetType() != typeof(InteractableSignPostData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = type;
        this.signText = ((InteractableSignPostData)interactableData).GetSignText();
        return true;
    }

    public override bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        //this.signText = "Hi";
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string signText) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = InteractionType.ORTHOGRAPHIC;
        this.signText = signText;
        return true;
    }
}
