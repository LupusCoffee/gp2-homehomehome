using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPCData : InteractableData {
    [SerializeField] private string npcName = "Default_NPC";
    [SerializeField] private List<string> dialogues = new List<string>();
    [SerializeField] private GameObject dialogueBoxPopup = null;

    public InteractableNPCData() : base(InteractionType.VERBAL) {
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(int interactableID) : base(interactableID, InteractionType.VERBAL) {
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(InteractionType type) : base(InteractionType.VERBAL) {
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(string npcName) : base(InteractionType.VERBAL) { 
        this.npcName = npcName;
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(List<string> dialogues) : base(InteractionType.VERBAL) { this.dialogues = dialogues; }

    public InteractableNPCData(string npcName, List<string> dialogues) : 
            base(InteractionType.VERBAL) { 
        this.npcName = npcName;
        ChangeAllDialogue(dialogues);
    }

    public InteractableNPCData(int interactableID, string npcName) : base(interactableID, InteractionType.VERBAL) {
        this.npcName = npcName;
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(int interactableID, string npcName, List<string> dialogues) : 
            base(interactableID, InteractionType.VERBAL) {
        this.npcName = npcName;
        this.ChangeAllDialogue(dialogues);
    }

    public InteractableNPCData(InteractionType type, string npcName) : base(InteractionType.VERBAL) {
        this.npcName = npcName;
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(int interactableID, InteractionType type) : base(interactableID, InteractionType.VERBAL) {
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(int interactableID, InteractionType type, string npcName) : 
            base(interactableID, InteractionType.VERBAL) {
        this.npcName = npcName;
        this.dialogues.Add("Empty_dialogue");
    }

    public InteractableNPCData(int interactableID, InteractionType type, string npcName, List<string> dialogues) :
            base(interactableID, InteractionType.VERBAL) {
        this.npcName = npcName;
        this.ChangeAllDialogue(dialogues);
    }


    public override IInteractableData GetInteractableData() { return this; }

    public override void SetInteractionType(InteractionType type) {
        //this.type = InteractionType.VERBAL;
    }

    public string GetNPCName() { return this.npcName; }

    public string GetFirstDialogue() {
        return this.dialogues[0];
    }

    public string GetDialogue(int index){
        if (index < 0 || index >= this.dialogues.Count) { return "Index out of bounds."; }
        else { return this.dialogues[index]; }
    }

    public List<string> GetDialogues() { return this.dialogues; }

    public void SetNPCName(string npcName) { this.npcName = npcName; }

    public bool ChangeAllDialogue(List<string> dialogues) {
        if (this.dialogues.Count != 0) {
            this.dialogues = dialogues;
            return true;
        }
        else
            this.dialogues.Add("Empty_dialogue");
        return false;
    }

    public void AddDialogue(string dialogue) { this.dialogues.Add(dialogue); }

    public bool RemoveDialogue(int index) {
        if (index < 0 || index >= this.dialogues.Count) { return false; }
        this.dialogues.RemoveAt(index);
        if (this.dialogues.Count == 0) { this.dialogues.Add("Empty_dialogue"); }
        return true;
    }

    public void RemoveAllDialogue() {
        this.dialogues.Clear();
        this.dialogues.Add("Empty_dialogue");
    }

    public override bool ChangeData(IInteractableData interactableData) {
        if (interactableID < 0 && interactableData.GetType() != typeof(InteractableNPCData)) { return false; }
        this.interactableID = interactableData.GetID();
        //this.type = type;
        this.npcName = ((InteractableNPCData)interactableData).GetNPCName();
        this.dialogueBoxPopup = ((InteractableNPCData)interactableData).GetDialogueBoxPopup();
        return true;
    }

    public override bool ChangeData(int interactableID, InteractionType type) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        //this.npcName = "Default_npc_name";
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string npcName) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        this.npcName = npcName;
        return true;
    }

    public bool ChangeData(int interactableID, InteractionType type, string npcName, List<string> dialogues) {
        if (interactableID < 0) { return false; }
        this.interactableID = interactableID;
        //this.type = type;
        this.npcName = npcName;
        ChangeAllDialogue(dialogues);
        return true;
    }

    public GameObject GetDialogueBoxPopup() { return dialogueBoxPopup; }

    public void SetDialogueBoxPopup(GameObject dialogueBoxPopup) { 
        this.dialogueBoxPopup = dialogueBoxPopup;
    }
}
