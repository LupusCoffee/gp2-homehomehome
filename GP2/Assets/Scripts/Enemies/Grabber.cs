using UnityEngine;
using UnityEngine.InputSystem;

public class Grabber : Enemy {
    private bool active = false;
    Vector3 playerRootOffset = new Vector3(0, 0.8f, 0);

    bool playerInRange;

    protected override void Start() {
        base.Start();
        this.player = Player.Instance; //is this needed due to Enemy superclass?
    }


    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerInRange = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && this.active == true) {
           this.player.gameObject.transform.position = transform.position + this.playerRootOffset;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player") {
            playerInRange = false;
        }
    }

    public void OnInteract(InputValue val)
    {
        if (playerInRange)
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

    public override void Activate() {
        this.active = true;
        Debug.Log("Grabber activated");

        this.player.SetGrabbed(true);
        player.transform.position = transform.position + playerRootOffset;
        //LeanTween.move(this.player.gameObject, transform.position + this.playerRootOffset, 0.5f);
        Debug.Log("Player is grabbed");
    }

    public override void Deactivate() {
        this.active = false;
        Debug.Log("Grabber deactivated");

        this.player.SetGrabbed(false);
        Debug.Log("Player is not grabbed");
        
    }
}
