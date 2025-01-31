using System.Collections;
using UnityEngine;

public abstract class Enemy : PausableMonoBehaviour {
    protected Player player;
    protected bool dazed;
    protected float dazeDuration = 1;
    protected Coroutine dazeCoroutine;
    protected Rigidbody rb;

    protected virtual void Start() {
        player = Player.Instance;
        rb = GetComponentInChildren<Rigidbody>();
    }

    public virtual void Activate()
    {

    }

    public virtual void Deactivate()
    {

    }

    public virtual void Daze()
    {
        dazed = true;

        if (dazeCoroutine != null)
            StopCoroutine(dazeCoroutine);

        dazeCoroutine = StartCoroutine(EndDaze(dazeDuration));
    }

    IEnumerator EndDaze(float timer)
    {
        yield return new WaitForSeconds(timer);
        dazed = false;
        rb.linearVelocity = Vector3.zero;
    }

}
