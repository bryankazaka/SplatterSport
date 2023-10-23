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
    private float moveUp;
    private float stunUp;
    private float startStun;
    private Vector2 stunDirect;
    private PlayerController playerController;
    private Vector2 screenBounds;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        
        screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    public void upgradeMoveSpeed(float upgrade)
    {
        moveUp = upgrade;
    }
    public void upgradeStunTime(float upgrade)
    {
        stunUp = upgrade;
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
        if (isStunned && (Time.time - startStun) > (playerController.stunTime*stunUp))
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
            Vector2 newPosition = rb.position + movement * (moveSpeed*moveUp) * Time.fixedDeltaTime;
            
            // Clamp the player's position within the screen boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x, screenBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y, 3);
            
            rb.MovePosition(newPosition);
        }
        else
        {
            Vector2 newPosition = rb.position + (Vector2)Vector3.Normalize(rb.position-stunDirect) * 
            (1/(2*(Time.time - startStun))) * Time.fixedDeltaTime;
           
            newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x, screenBounds.x);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y, 3);
            
            rb.MovePosition(newPosition);
        }
        
        
        
        

    }
    
}
