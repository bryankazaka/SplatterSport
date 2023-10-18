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
    public float attackSpeed = 1.00f;
    private float startAttackTime;
    private bool isAttacking;
    private bool attackHold;
    private Transform playerTransform;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private Animator animator;
    public RuntimeAnimatorController newAnimatorController;
    private SpriteRenderer sRenderer;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    private Weapon weapon;
    private SetWeaponType setWeaponType;
    private PlayerController playerController;
    private List<Collider2D> hitEnemies = new List<Collider2D>();
    public InputActionAsset inputActionAsset;


    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        sRenderer = GetComponent<SpriteRenderer>();
        playerTransform = transform.parent;
        animator = GetComponent<Animator>();
        setWeaponType = GetComponent<SetWeaponType>();
        initWeapon(playerController.colour,playerController.weapon);
        maxRange = attackRange;


    
    }
     

    private void FixedUpdate()
    {
        Vector3 target;
        if (Input.mousePresent)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint( Mouse.current.position.ReadValue());
            mouse = new Vector3(mouse.x,mouse.y,0);
            target = mouse - playerController.transform.position;
        }
        else
        {
            target = new(0,0,0); // Add The vector of the aim here Cameron
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


        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Swing"))
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
            Debug.Log( MathF.Sin((Time.time - startAttackTime)/lenA * math.PI));       
            attackRange = MathF.Sin((Time.time - startAttackTime)/lenA * math.PI) * maxRange;  

        }
        else
        {
            if (hitEnemies.Count > 0){
                hitEnemies.Clear();
            }

        }
        
       
        if (Input.GetButtonDown("Fire1") || Input.GetButton("Fire1"))
        {

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Swing")) // if I press and its not attacking attack
            {
                Attack();
                          
            }          
           
        }

    }

    public void initWeapon(int colour, int weaponType)
    {
        // setWeaponType = GetComponent<SetWeaponType>();
        switch (weaponType)
        {
            case BRUSH:
                weapon = new Brush(colour);
                setWeaponType.Set(BRUSH);
                //set colour of weapon
                break;
            case PENCIL:
                weapon = new Pencil(colour);
                setWeaponType.Set(PENCIL);
                //set colour of weapon
                break;
            case ROLLER:
                weapon = new Roller(colour);
                setWeaponType.Set(ROLLER);
                //set colour of weapon
                break;
            default:
                weapon = new Brush(colour);
                setWeaponType.Set(BRUSH);
                break;
        }

        attackRange = weapon.getRange();
        //attackSpeed = weapon.getAttackSpeed();
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
        
        animator.SetTrigger("Attack");
        startAttackTime = Time.time;      

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

     float GetAnimationClipLengthByName(string clipName)
    {
        float length = 0f;
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0); // Layer 0
        foreach (var info in clipInfo)
        {
            if (info.clip.name == clipName)
            {
                length = info.clip.length;
                break;
            }
        }
        return length;
    }
}
