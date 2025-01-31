using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

// This comment is necessary for not breaking the game
public class Player : MonoBehaviour
{
    public static Player Instance;

    public enum PlayerState { Idle, Walking, PlayingMusic, Crouching, Rooted }
    public PlayerState playerState = PlayerState.Idle;
    bool isMoving;

    PlayerController movement;

    PlayerInput playerInput;
    CinemachineCamera vcam;
    CinemachineOrbitalFollow vcamFollow;

    float horizontalAxisBeforePause = 0;
    float verticalAxisBeforePause = 0;

    [SerializeField] GameObject spellParticles;

    Vector2 mousePosBeforeOpenMenu = new Vector2();

    bool isPlayingMusic = false; //nah bruh we gotta fix this input system thing lmao

    public Rigidbody rb { get; private set; }

    public bool isOutOfBody { get; private set; }
    public GameObject currentPlayerBody { get; private set; }

    Animator animator;
    [SerializeField] MeshRenderer fluteRenderer;

    SpellMaker spellMaker;

    private void Awake()
    {
        Instance = this;

        if (playerState == PlayerState.Walking || playerState == PlayerState.Crouching)
            this.isMoving = true;
        else
            this.isMoving = false;

        movement = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
        vcam = GetComponentInChildren<CinemachineCamera>();
        vcamFollow = vcam.GetComponent<CinemachineOrbitalFollow>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        spellMaker = GetComponent<SpellMaker>();

        currentPlayerBody = gameObject;
    }

    private void Update()
    {
        animator.SetInteger("PlayerState", (int)playerState);

        if (playerState == PlayerState.PlayingMusic)
            fluteRenderer.enabled = true;
        else
            fluteRenderer.enabled = false;



        if (playerState == PlayerState.Rooted || isOutOfBody)
            return;

        this.isMoving = UserInputs.Instance.playerMove != Vector2.zero;

        if (this.isMoving)
        {
            playerState = PlayerState.Walking;
        }
        else
        {
            if (spellMaker.NoteSheetActive)
                playerState = PlayerState.PlayingMusic;

            if (playerState != PlayerState.PlayingMusic)
                playerState = PlayerState.Idle;
        }
    }




    public void SetGrabbed(bool rooted = true)
    {
        movement.isRooted = rooted;

        if (rooted)
            playerState = PlayerState.Rooted;
        else
            playerState = PlayerState.Idle;
    }

    public void EnablePlayerControls()
    {
        playerInput.enabled = true;
        vcam.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;

        vcamFollow.HorizontalAxis.Value = horizontalAxisBeforePause;
        vcamFollow.VerticalAxis.Value = verticalAxisBeforePause;

    }

    public void DisablePlayerControls()
    {
        Debug.Log("DisablePlayerControls() called.");
        playerInput.enabled = false;
        vcam.enabled = false;

        Cursor.lockState = CursorLockMode.None;

        horizontalAxisBeforePause = vcamFollow.HorizontalAxis.Value;
        verticalAxisBeforePause = vcamFollow.VerticalAxis.Value;
    }


    //we gotta fix this input system thing lmao - created by Mohammed
    public PlayerController GetController() => movement;
    public void SetIsPlayingMusic(bool value)
    {
        isPlayingMusic = value;

        if (isPlayingMusic)
            playerState = PlayerState.PlayingMusic;
        else
            playerState = PlayerState.Idle;
    }

    public void DisableControls(GameObject newTarget)
    {
        playerState = PlayerState.Rooted;
        vcam.Target.TrackingTarget = newTarget.transform;
        isOutOfBody = true;

        currentPlayerBody = newTarget;

        UserInputs.Instance.OnMindControlStart();
    }

    public void EnableControls()
    {
        playerState = PlayerState.Idle;
        vcam.Target.TrackingTarget = transform;
        isOutOfBody = false;

        currentPlayerBody = gameObject;

        UserInputs.Instance.OnMindControlEnd();

        spellMaker.CloseNoteSheet();
    }

    public void PlaySpellParticles()
    {
        GameObject temp = Instantiate(spellParticles, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
        temp.transform.SetParent(transform, true);
    }
}
