using UnityEngine;
using static SO_SpellAttributes;

public class InteractableSpellAttributeData : InteractableCollectableData {
    //private string loreText = "Default_Lore_Text";
    private SO_SpellAttribute so = null;

    public InteractableSpellAttributeData() : base("Default_SpellAttribute") { this.type = InteractionType.SPELL_ATTRIBUTE; }

    public InteractableSpellAttributeData(int interactableID) : base(interactableID, "Default_SpellAttribute") {
        this.type = InteractionType.SPELL_ATTRIBUTE;
    }

    /*public SpellAttributeData(InteractionType type) : base("Default_SpellAttribute") {
        this.type = InteractionType.SPELL_ATTRIBUTE;
    }*/

    public InteractableSpellAttributeData(string spellAttributeName, SO_SpellAttribute so) : base(spellAttributeName) {
        this.type = InteractionType.SPELL_ATTRIBUTE;
        this.so = so;
    }

    public InteractableSpellAttributeData(int interactableID, string spellAttributeName, SO_SpellAttribute so) : 
            base(interactableID, spellAttributeName) {
        this.type = InteractionType.SPELL_ATTRIBUTE;
        this.so = so;
    }

    public InteractableSpellAttributeData(InteractionType type, string spellAttributeName, SO_SpellAttribute so) : 
            base(spellAttributeName) {
        this.type = InteractionType.SPELL_ATTRIBUTE;
        this.so = so;
    }

    public InteractableSpellAttributeData(int interactableID, InteractionType type) : 
            base(interactableID, "Default_SpellAttribute") {
        this.type = InteractionType.SPELL_ATTRIBUTE;
    }

    public InteractableSpellAttributeData(int interactableID, InteractionType type, string spellAttributeName, 
            SO_SpellAttribute so) : base(interactableID, spellAttributeName) {
        this.type = InteractionType.SPELL_ATTRIBUTE;
        this.so = so;
    }


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.SPELL_ATTRIBUTE;
    }

    public SO_SpellAttribute GetScriptableObject() { return this.so; }

    public bool GetScriptableObject(SO_SpellAttribute so) {
        this.so = so;
        return true;
    }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableData.GetID() < 0 && interactableData.GetType() != typeof(InteractableSpellAttributeData)) { return false; }
        this.interactableID = interactableData.GetID();
        this.collectableName = ((InteractableSpellAttributeData)interactableData).GetCollectableName();
        this.so = ((InteractableSpellAttributeData)interactableData).GetScriptableObject();
        return true;
    }

    public override bool ChangeData(int interactableID, InteractionType type, string spellAttributeName) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        this.collectableName = spellAttributeName;
        //Find a scriptable object by it's name???
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string spellAttributeName, SO_SpellAttribute so) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        this.collectableName = spellAttributeName;
        this.so = so;
        return true;
    }
}
