using UnityEngine;

public class Puzzle_Movable : SpellActivatable
{
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Vector3 initialRotation;

    private Vector3 alternatePosition;

    [SerializeField] bool alterPosition = false;
    [SerializeField] bool alterScale = false;
    [SerializeField] bool alterRotation = false;

    [SerializeField] private Vector3 alternatePositionOffset;
    [SerializeField] private Vector3 alternateScale = new Vector3(1, 1, 1);
    [SerializeField] private Vector3 alternateRotation;

    [SerializeField] float moveSpeed = 1f;

    bool isMoved = false;

    private void Awake()
    {
        LeanTween.reset();
    }

    private void Start()
    {
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.rotation.eulerAngles;
        alternatePosition = initialPosition + alternatePositionOffset;
    }

    public override void ActivateBySpell()
    {
        Debug.Log("Moved nature object " + Random.Range(0, 10000000));
        LeanTween.cancel(gameObject);

        if (isMoved)
        {
            Debug.Log("Moved object to initial position");
            if (alterPosition) LeanTween.move(gameObject, initialPosition, moveSpeed);
            if(alterScale) LeanTween.scale(gameObject, initialScale, moveSpeed);
            if(alterRotation) LeanTween.rotate(gameObject, initialRotation, moveSpeed);
        }
        else
        {
            Debug.Log("Moved object to altered position " + alternatePosition + " original pos is " + initialPosition + " speed is " + moveSpeed);
            if (alterPosition) LeanTween.move(gameObject, alternatePosition, moveSpeed);
            if (alterScale) LeanTween.scale(gameObject, alternateScale, moveSpeed);
            if (alterRotation) LeanTween.rotate(gameObject, alternateRotation, moveSpeed);
        }

        isMoved = !isMoved;
    }

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}
