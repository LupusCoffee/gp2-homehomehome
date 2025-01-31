using System;
using System.Threading.Tasks;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : PausableMonoBehaviour
{
    Rigidbody rb;

    float movementSpeed = 5.0f;
    float rotationSpeed = 700.0f;

    [SerializeField] LayerMask groundMask;
    bool isGrounded = false;

    Vector2 moveInput = Vector2.zero;

    PlayerInteractZone interactZone;

    public bool isRooted;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    public LayerMask GroundMask { get => groundMask; set => groundMask = value; }

    float velocity;
    public float Velocity { get => velocity; }

    bool isPlayingMusic = false; //nah bruh we gotta fix this input system thing lmao

    public Vector3 MovementDirection { get; private set; }

    bool isDestroyed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        interactZone = GetComponentInChildren<PlayerInteractZone>();

    }

    public override void OnEnable()
    {
        UserInputs.Instance._playerInteract.performed += OnInteract;
    }

    public override void OnDisable()
    {
        UserInputs.Instance._playerInteract.performed -= OnInteract;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        isGrounded = GroundedCheck();

        moveInput = UserInputs.Instance.playerMove;
        if (moveInput != Vector2.zero && !isRooted
            && !IsPaused && !Player.Instance.isOutOfBody)
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

            float horizontalInput = moveInput.x;
            float verticalInput = moveInput.y;

            if (IsAboutToWalkIntoAWall(flatForward * verticalInput + Camera.main.transform.right * horizontalInput))
            {
                return;
            }

            Vector3 moveDirection = flatForward * verticalInput + Camera.main.transform.right * horizontalInput;

            float airMultiplier = isGrounded ? 1.0f : 0.8f;
            float crouchMultiplier = Player.Instance.playerState == Player.PlayerState.Crouching ? 0.5f : 1.0f;
            transform.position += moveDirection * movementSpeed * airMultiplier * crouchMultiplier * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate() {
        GetDistanceMovedFromLastFrame(transform.position);
    }


    private bool GroundedCheck() {
        return Physics.Raycast(transform.position, Vector3.down, 1.05f, groundMask);
    }

    public void OnInteract(InputAction.CallbackContext obj) {
        Debug.Log("Interacting...");

        if (isPlayingMusic || Player.Instance.isOutOfBody) return;

        /*if (UserInputs.Instance.playerInteract)
        {
            
        }*/
        Interactable interactable = interactZone.GetInteractable();
        if (interactable != null) {
            IInteractableData result = interactable.Interact();

            if (result != null) {
                switch (interactable) {
                    case InteractableArtifact artifact:
                        Debug.Log(artifact.ToString());
                        Inventory.TryCollectItem(artifact); // Collect the item - Added by Martin M
                        artifact.GetGameObject().GetComponent<Collider>().enabled = false;
                        artifact.GetGameObject().GetComponent<MeshRenderer>().enabled = false;
                        //Destroy(interactable.GetGameObject());
                        break;
                    case InteractableSignPost signPost:
                        Debug.Log(signPost.ToString());
                        break;
                    case InteractableNPC npc:
                        Debug.Log(npc.ToString());
                        //Debug.Log(((InteractableNPCData)result).GetFirstDialogue());
                        break;
                    case InteractableTransition transition:
                        Debug.Log(transition.ToString());
                        break;
                    case InteractableDoor door:
                        Debug.Log(door.ToString());
                        break;
                    case InteractableSpellAttribute spellAttribute:
                        Debug.Log(spellAttribute.ToString());
                        Inventory.TryCollectItem(spellAttribute); // Collect the item - Added by Martin M
                        spellAttribute.GetGameObject().GetComponent<Collider>().enabled = false;
                        spellAttribute.GetGameObject().GetComponent<MeshRenderer>().enabled = false;
                        break;
                    default: // If nothing matches
                        Debug.Log(interactable.GetType().Name);
                        break;
                }
            }
        } else {
            Debug.Log("No interactable found");
        }

        Debug.Log(LanguageEncrypter.TranslateText("there is a fungus amungus", 7));
    }

    private bool IsAboutToWalkIntoAWall(Vector3 direction)
    {
        Vector3 footHeight = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Debug.DrawRay(footHeight, direction * 0.65F, Color.red, 10);
        return Physics.Raycast(footHeight, direction, 0.65F, groundMask, QueryTriggerInteraction.Ignore);
    }

    private async void GetDistanceMovedFromLastFrame(Vector3 lastPosition)
    {
        await Task.Delay(1);

        if (isDestroyed) return;

        velocity = Vector3.Distance(lastPosition, transform.position);
    }


    //we gotta fix this input system thing lmao - created by Mohammed
    public void SetIsPlayingMusic(bool value)
    {
        isPlayingMusic = value;
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }
}
