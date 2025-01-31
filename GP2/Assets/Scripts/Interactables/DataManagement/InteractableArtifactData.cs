using UnityEngine;

//public class ArtifactData : InteractableData {
public class InteractableArtifactData : InteractableCollectableData {
    private string loreText = "Default_Lore_Text";

    public InteractableArtifactData() : base(InteractionType.ITEM, "Default_Artifact") {}

    public InteractableArtifactData(int interactableID) : base(interactableID, InteractionType.ITEM, "Default_Artifact") {}

    public InteractableArtifactData(InteractionType type) : base(InteractionType.ITEM, "Default_Artifact") {}

    public InteractableArtifactData(string artifactName) : base(InteractionType.ITEM, artifactName) {}

    public InteractableArtifactData(int interactableID, string artifactName) : base(interactableID, InteractionType.ITEM,
            artifactName) {}

    public InteractableArtifactData(InteractionType type, string artifactName) : base(InteractionType.ITEM, artifactName) {}

    public InteractableArtifactData(int interactableID, InteractionType type) : base(interactableID, InteractionType.ITEM, 
            "Default_Artifact") {}

    public InteractableArtifactData(int interactableID, InteractionType type, string artifactName) : base(interactableID, 
            InteractionType.ITEM, artifactName) {}

    public InteractableArtifactData(InteractionType type, string artifactName, string loreText) : 
            base(InteractionType.ITEM, artifactName) {
        this.loreText = loreText;
    }

    public InteractableArtifactData(int interactableID, string artifactName, string loreText) : 
            base(interactableID, InteractionType.ITEM, artifactName) {
        this.loreText = loreText;
    }

    public InteractableArtifactData(int interactableID, InteractionType type, string artifactName, string loreText) : 
            base(interactableID, InteractionType.ITEM, artifactName) {
        this.loreText = loreText;
    }


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.ITEM;
    }

    public string GetLoreText() { return this.loreText; }

    public void SetLoreText(string loreText) { this.loreText = loreText; }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableData.GetID() < 0 && interactableData.GetType() != typeof(InteractableArtifactData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = InteractionType.ITEM;
        this.collectableName = ((InteractableArtifactData)interactableData).GetCollectableName();
        this.loreText = ((InteractableArtifactData)interactableData).GetLoreText();
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string collectableName, string loreText) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        this.collectableName = collectableName;
        this.loreText = loreText;
        return true;
    }
}
