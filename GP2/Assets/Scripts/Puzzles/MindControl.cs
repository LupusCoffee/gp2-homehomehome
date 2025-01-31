using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static CompactMath;
using static Player;

public class MindControl : PausableMonoBehaviour
{
    public bool isMindControlled = false;
    Vector2 moveInput;

    Rigidbody rb;

    [SerializeField] LayerMask groundMask;

    public PlayerState playerState = PlayerState.Idle;

    bool isMoving;

    float gracePeriod = 2;
    float gracePeriodTimer = 0;

    Enemy enemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        enemy = GetComponentInChildren<Enemy>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UserInputs.Instance._activateNoteSheet.performed += OnActivateNoteSheet;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UserInputs.Instance._activateNoteSheet.performed -= OnActivateNoteSheet;
    }

    public void ActivateMindControl()
    {
        isMindControlled = true;
        gracePeriodTimer = gracePeriod;
    }

    public void DeactivateMindControl()
    {
        isMindControlled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }


    private void Update()
    {
        if (gracePeriodTimer > 0) {
            gracePeriodTimer -= Time.deltaTime;
        }

        moveInput = UserInputs.Instance.playerMove;
        if (moveInput != Vector2.zero && isMindControlled && !IsPaused)
        {
            Vector3 flatForward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;

            float horizontalInput = moveInput.x;
            float verticalInput = moveInput.y;

            /*
            if (IsAboutToWalkIntoAWall(flatForward * verticalInput + Camera.main.transform.right * horizontalInput))
            {
                return;
            }
            */

            Vector3 moveDirection = flatForward * verticalInput + Camera.main.transform.right * horizontalInput;

            transform.position += moveDirection * 2 * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 150 * Time.deltaTime);
        }
    }

    public void OnMove()
    {
        moveInput = UserInputs.Instance.playerMove;
    }

    private bool IsAboutToWalkIntoAWall(Vector3 direction)
    {
        Vector3 footHeight = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Debug.DrawRay(footHeight, direction * 0.65F, Color.red, 10);
        return Physics.Raycast(footHeight, direction, 0.65F, groundMask, QueryTriggerInteraction.Ignore);
    }

    public void OnActivateNoteSheet(InputAction.CallbackContext ctx)
    {
        Debug.Log("Note sheet activated");
        if (isMindControlled && gracePeriodTimer <= 0)
        {
            DeactivateMindControl();
            Player.Instance.EnableControls();
            enemy.Deactivate();
        }
    }
}
