using UnityEngine;
using UnityEngine.InputSystem;
using static CompactMath;
using static MultiTag;

public class ThornshellMouth : Enemy
{
    [SerializeField] float throwStrength = 5;
    [SerializeField] private bool active = false;

    MindControl mindControl;

    private bool grabbableObjectInRange = false;
    GameObject objectToGrab;
    GameObject grabbedObject;

    float throwCooldown = 1;
    float throwTimer = 0;

    Transform objToMove;

    SphereCollider grabbedCollider;

    Rigidbody myRigidBody;

    public override void OnEnable()
    {
        base.OnEnable();
        UserInputs.Instance._playerInteract.performed += OnInteract;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        UserInputs.Instance._playerInteract.performed -= OnInteract;
    }

    private void Awake()
    {
        mindControl = GetComponentInParent<MindControl>();
        objToMove = transform.parent;

        grabbedCollider = GetComponentInParent<SphereCollider>();
    }

    private void Update()
    {
        if (throwTimer > 0) {
            throwTimer -= Time.deltaTime;
        }

        //move player along with self if attached
        if (this.active)
        {
            Vector3 grabbedPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            grabbedObject.transform.position = grabbedPos + transform.forward.normalized;
            grabbedObject.transform.rotation = transform.rotation;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(HasTag(other.gameObject, MultiTags.Player) || HasTag(other.gameObject, MultiTags.GrabbableObject))
        {
            grabbableObjectInRange = true;
            objectToGrab = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.active && (HasTag(other.gameObject, MultiTags.Player) || HasTag(other.gameObject, MultiTags.GrabbableObject)))
        {
            grabbableObjectInRange = false;
            objectToGrab = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (!mindControl.isMindControlled) return;

        Debug.Log("test thornshell interact");
        if (grabbableObjectInRange)
        {
            if (this.active)
            {
                Deactivate();
            }
            else
            {
                Activate();
            }
        }
    }

    public override void Activate()
    {
        grabbedObject = objectToGrab;

        this.active = true;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.GetComponent<Collider>().enabled = false;
        grabbedObject.transform.SetParent(gameObject.transform, true);

        grabbedCollider.enabled = true;
    }

    public override void Deactivate()
    {
        if (throwTimer > 0) return;

        if(active) {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Collider>().enabled = true;

            grabbedObject.transform.SetParent(null, true);


            Vector3 direction = transform.forward + Vector3.up;

            if(grabbedObject.TryGetComponent(out Rigidbody rb))
            {
                rb.angularVelocity = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                rb.AddForce(direction * throwStrength, ForceMode.Impulse);
            }
        }

        throwTimer = throwCooldown;
        grabbedObject = null;
        grabbableObjectInRange = false;
        this.active = false;

        grabbedCollider.enabled = false;
    }
}
