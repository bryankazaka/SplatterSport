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
    private float startStun;
    private Vector2 stunDirect;
    private PlayerController playerController;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement.x = ctx.ReadValue<Vector2>().x;
        movement.y = ctx.ReadValue<Vector2>().y;
        // movement.z = 0; 
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stunDirect = collision.gameObject.transform.position;
        startStun = Time.time;
        isStunned = true;
        animator.SetBool("Stunned",true);
    }
    //function that runs every frame (like update) but is better for physics stuff
    void Update()
    {
        if (isStunned && (Time.time - startStun) > playerController.stunTime)
        {
            isStunned = false;
            animator.SetBool("Stunned", false);
        }
    }

    void FixedUpdate()
    {
        //Movement
        if (!isStunned)
        {
            animator.SetFloat("Horizontal", movement.x); 
            animator.SetFloat("Vertical", movement.y); 
            animator.SetFloat("Speed", movement.sqrMagnitude);
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
             rb.MovePosition(rb.position + (Vector2)Vector3.Normalize(rb.position-stunDirect) * 
             (1/(2*(Time.time - startStun))) * Time.fixedDeltaTime);
        }
        
        
        
        

    }
    
}
