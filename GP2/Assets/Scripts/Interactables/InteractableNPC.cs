using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : Interactable {
    //[SerializeField] GameObject dialogueBoxPopup;

    public InteractableNPC() : base(new InteractableNPCData()) {}

    public InteractableNPC(InteractableNPCData data) : base(data) {}

    public InteractableNPC(int interactableID) : base(new InteractableNPCData(interactableID)) {}

    public InteractableNPC(string npcName) : base(new InteractableNPCData(npcName)) {}

    public InteractableNPC(string npcName, List<string> dialogues) : base(new InteractableNPCData(npcName, dialogues)) {}

    public InteractableNPC(InteractableNPCData data, InteractionState state) : base(data, state) {}

    void Update() {}

    public override IInteractableData Interact() {
        /*GameObject gameObject = Instantiate(((InteractableNPCData)this.data.GetInteractableData()).GetDialogueBoxPopup(), 
            transform.position + new Vector3(0, 2.5F, 0), Quaternion.identity);
        gameObject.transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, Vector3.one, 0.25f);
        StartCoroutine(TextDecay(gameObject));*/

        return this.data;
    }

    private IEnumerator TextDecay(GameObject target) {
        yield return new WaitForSeconds(3);
        LeanTween.scale(target, Vector3.zero, 0.1f).setOnComplete(()
            => Destroy(target));
    }

    public override bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override void SetInteractionType(InteractionType type) {
        //this.data.SetInteractionType(InteractionType.TRANSITION_TO_AREA);
    }

    public override string ToString() {
        return ("[InteractableNPC, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + ", NPCName=" + ((InteractableNPCData)this.data).GetNPCName() + "]");
    }
}
