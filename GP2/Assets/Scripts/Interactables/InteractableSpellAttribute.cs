using UnityEngine;

public class InteractableSpellAttribute : InteractableCollectable {
    public InteractableSpellAttribute() : base(new InteractableSpellAttributeData()) {}

    public InteractableSpellAttribute(InteractableSpellAttributeData data) : base(data) {}

    public InteractableSpellAttribute(int interactableID) : base(new InteractableSpellAttributeData(interactableID)) {}

    public InteractableSpellAttribute(string spellAttributeName, SO_SpellAttribute so) : 
            base(new InteractableSpellAttributeData(spellAttributeName, so)) {}

    public InteractableSpellAttribute(InteractableSpellAttributeData data, InteractionState state) : base(data, state) {}

    public InteractableSpellAttribute(string spellAttributeName, SO_SpellAttribute so, InteractionState state) :
            base(new InteractableSpellAttributeData(spellAttributeName, so), state) {}

    protected override void Start() {
        base.Start();
        //this.type = InteractionType.ITEM;
        Debug.Log($"ID {GetID()}");
    }

    void Update() {

    }

    public override IInteractableData Interact() { return this.data; }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.SpellAttributeData);
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[InteractableSpellAttribute, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", SpellAttributeName=" + 
            ((InteractableSpellAttributeData)this.data).GetCollectableName() + "]");
    }
}
