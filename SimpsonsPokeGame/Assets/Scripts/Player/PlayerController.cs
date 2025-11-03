using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float moveSpeed = 4f;
    Rigidbody2D rb;
    Vector2 move;
    Animator animator;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        
        // normalize for diagonal speed
        if (move.sqrMagnitude > 1) move.Normalize();
        
        // update animator
        if (animator != null) {
            animator.SetFloat("MoveX", move.x);
            animator.SetFloat("MoveY", move.y);
            animator.SetFloat("Speed", move.sqrMagnitude);
        }
        
        // interact on Space
        if (Input.GetKeyDown(KeyCode.Space)) {
            TryInteract();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    void TryInteract() {
        // raycast forward to detect NPCs or interactable objects
        Vector2 direction = new Vector2(move.x, move.y);
        if (direction.sqrMagnitude == 0) {
            direction = new Vector2(transform.localScale.x, 0);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.5f);
        if (hit.collider != null) {
            INPCInteractable interactable = hit.collider.GetComponent<INPCInteractable>();
            if (interactable != null) {
                interactable.Interact();
            }
        }
    }
}
