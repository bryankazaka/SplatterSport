using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;

public class WeaponController : MonoBehaviour
{
    const int GREEN = 1, YELLOW = 2, RED = 3, BLUE = 4;
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;
    
    public float weaponRange = 1f;
    public float minWeaponRange = 0.5f;
    public float attackRange = 0.5f;
    public float attackSpeed = 1.00f;
    
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

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        sRenderer = GetComponent<SpriteRenderer>();
        playerTransform = transform.parent;
        animator = GetComponent<Animator>();
        setWeaponType = GetComponent<SetWeaponType>();
        initWeapon(playerController.colour,playerController.weapon);
        print("here");
    }
    
    private void FixedUpdate()
    {
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse = new Vector3(mouse.x,mouse.y,0);
        Vector3 target = mouse - playerController.transform.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        target = Vector3.Normalize(target)*1.5f;
        Debug.Log(target);
        target = playerController.transform.position + target;
        transform.position = target;
        
       
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
        
        
        // Apply the rotation to the child GameObject (the one with the sprite)
        transform.rotation = Quaternion.Euler(0, 0, angle);


        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == Animator.StringToHash("Roller_Swing"))
        {
            print("attacking");
            List<Collider2D> currentHits = (Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers)).ToList();
            foreach (Collider2D hit in currentHits)
            {
                if(!hitEnemies.Contains(hit)){
                    hitEnemies.Add(hit);
                    damageEnemy(hit);
                }
            }
        }
        else
        {
            if (hitEnemies.Count > 0){
                hitEnemies.Clear();
            }

        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Swing"))
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
                weapon = new Brush(GREEN);
                setWeaponType.Set(BRUSH);
                break;
        }

        attackRange = weapon.getRange();
        attackSpeed = weapon.getAttackSpeed();

    }
    
    private void damageEnemy(Collider2D hit)
    {
        float dmgAmount = weapon.getDamage() * playerController.damageMult;
        //deal dmg to hit enemy
    }

    public void Attack()
    {
        
        animator.SetTrigger("Attack");

    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
