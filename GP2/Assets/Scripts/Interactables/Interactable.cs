using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable {
    [SerializeField] protected GameObject go;
    [SerializeField] protected IInteractableData data;
    [SerializeField] protected InteractionState state = InteractionState.ON;

    public Interactable() {
        this.data = new InteractableData();
        this.state = InteractionState.ON;
    }

    public Interactable(IInteractableData data) {
        this.data = data;
        this.state = InteractionState.ON;
    }

    public Interactable(int interactableID) {
        this.data = new InteractableData(interactableID);
        this.state = InteractionState.ON;
    }

    public Interactable(InteractionType type) {
        this.data = new InteractableData(type);
        this.state = InteractionState.ON;
    }

    public Interactable(int interactableID, InteractionType type) {
        this.data = new InteractableData(interactableID, type);
        this.state = InteractionState.ON;
    }

    public Interactable(IInteractableData data, InteractionState state) {
        this.data = data;
        this.state = state;
    }

    public Interactable(int interactableID, InteractionType type, InteractionState state) {
        this.data = new InteractableData(interactableID, type);
        this.state = state;
    }

    protected virtual void Start() {
        // Initialize the ID from the database - Must be called in Start() to ensure the database is loaded
        this.InitializeIDFromDatabase();
    }

    void Update() {
        
    }

    public virtual IInteractableData Interact() {
        return this.data;
    }

    public IInteractableData GetInteractableData() {
        return this.data;
    }

    public GameObject GetGameObject() { return this.go; }

    public void Activate() { this.state = InteractionState.ON; }

    public void Deactivate() { this.state = InteractionState.OFF; }

    public InteractionType GetInteractionType() {
        return this.data.GetInteractionType();
    }

    public InteractionState GetInteractionState() {
        return state;
    }

    public virtual bool SetID(int interactableID) {
        return this.data.SetID(interactableID);
    }

    public int GetID() {
        return this.data.GetID();
    }

    public virtual void SetInteractionType(InteractionType type) { 
        this.data.SetInteractionType(type);
    }

    public virtual bool ChangeState(InteractionState interactionState) {
        this.state = interactionState;
        return true;
    }

    public override string ToString() {
        return ("[Interactable, ID=" + this.data.GetID() + ", InteractionType=" + this.data.GetInteractionType() +
            ", InteractionState=" + this.state + "]");
    }
}
