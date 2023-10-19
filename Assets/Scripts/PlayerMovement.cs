using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isStunned = false;
    public Rigidbody2D rb;
    public Animator animator;
    private PlayerController playerController;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement.x = ctx.ReadValue<Vector2>().x;
        movement.y = ctx.ReadValue<Vector2>().y;
        // movement.z = 0; 
    } 

    //function that runs every frame (like update) but is better for physics stuff
    void Update()
    {
        if (!isStunned)
        {
            animator.SetFloat("Horizontal", movement.x); 
            animator.SetFloat("Vertical", movement.y); 
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
    
}
