using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;
    public float weaponRange = 1f;
    private float maxRange ;
    public float minWeaponRange = 0.5f;
    public float attackRange = 0.5f;
    public float attackSpeed;
    private float startAttackTime;
    private bool isAttacking;
    private Transform playerTransform;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private Animator animator;

    
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    private Weapon weapon;
    // private SetWeaponType setWeaponType;
    private PlayerController playerController;
    private List<Collider2D> hitEnemies = new List<Collider2D>();

    public PlayerInput playerInput;
    public bool isMouse;
    private Vector2 joystickDirection;
    private  Vector3 target;

    private void Awake()
    {
           
        
        
       
        weapon = new Brush(0);
    }

    private void Start()
    {
    
        playerController = GetComponentInParent<PlayerController>();       
        playerTransform = transform.parent;
                 
        maxRange = attackRange;
        playerInput = GetComponentInParent<PlayerInput>();
        isMouse = playerInput.currentControlScheme == "Keyboard";        
        
        

    }

    private void FixedUpdate()
    {
      
        if (isMouse)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
            mouse = new Vector3(mouse.x,mouse.y,0);
            target = mouse - playerController.transform.position;
        }
        else
        {
            var tempTarget = new Vector3(joystickDirection.x,joystickDirection.y,0);
            if (!(tempTarget == new Vector3(0,0,0)))
            {
                target = tempTarget;
            }
            else
            {
                target = new(1,0,0);
            }

           
            
        }
       
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        target = Vector3.Normalize(target)*1.5f;
        target = playerController.transform.position + target;
        transform.position = target;
        attackPoint.position = target;
        
       
        if (math.abs(angle) > 90)
        {         
            gameObject.GetComponent<SpriteRenderer>().flipY = true;     
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (angle > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
       
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Swing"))
        {
            
            List<Collider2D> currentHits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers).ToList();
            foreach (Collider2D hit in currentHits)
            {
                if(!hitEnemies.Contains(hit)){
                    hitEnemies.Add(hit);
                    damageEnemy(hit);
                }
            }
            var lenA = animator.GetCurrentAnimatorStateInfo(0).length;
            if (( Time.time - startAttackTime ) >= lenA)
            {
                startAttackTime = Time.time;                
            }               
              
            attackRange = MathF.Sin((Time.time - startAttackTime)/lenA * math.PI) * maxRange;  

        }
        else
        {
            if (hitEnemies.Count > 0){
                hitEnemies.Clear();
            }

        }
        
        if (isAttacking && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Swing"))
        {
            
            Attack();
        } 
       

       
    }


    public void initWeapon(int colour, int weaponType, Sprite spriteT)
    {
         
        
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteT;
       
        animator = GetComponent<Animator>();
        switch (weaponType)
        {
            case BRUSH:
                weapon = new Brush(colour);
                animator.SetInteger("Weapon",BRUSH);
                animator.SetInteger("Colour",colour);
                //set colour of weapon
                break;
            case PENCIL:
                weapon = new Pencil(colour);
                animator.SetInteger("Weapon",PENCIL);
                animator.SetInteger("Colour",colour);
                //set colour of weapon
                break;
            case ROLLER:
                weapon = new Roller(colour);
                animator.SetInteger("Weapon",ROLLER);
                animator.SetInteger("Colour",colour);
                //set colour of weapon
                break;
        }
         attackRange = weapon.getRange();
        attackSpeed = weapon.getAttackSpeed();
        animator.SetFloat("Speed",attackSpeed);
        
    }

    private void damageEnemy(Collider2D hit)
    {
   
        float dmgAmount = weapon.getDamage() * playerController.damageMult;
        hit.gameObject.GetComponent<MobController>().takeDamage(playerController.gameObject, dmgAmount);
        //deal dmg to hit enemy
    }

    public void Attack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Swing")) // if I press and its not attacking attack
        {
            animator.SetTrigger("Attack");
            startAttackTime = Time.time;      
        }

    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        
       Debug.Log("Attack");
        if (ctx.started)
        {
            Debug.Log("MouseButtonDown");
            isAttacking = true;
        }
        if (ctx.canceled)
        {
             Debug.Log("MouseButtonUp");
            isAttacking = false;
        }
       
        
    }
    public void OnAim(InputAction.CallbackContext ctx) => joystickDirection = ctx.ReadValue<Vector2>();

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


}
