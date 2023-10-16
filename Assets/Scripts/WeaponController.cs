using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    const int GREEN = 1, YELLOW = 2, RED = 3, BLUE = 4;
    public float offset = 1f;
    public float weaponRange = 1f;
    public float minWeaponRange = 0.5f;

    public float attackRange = 0.5f;
    public Vector3 pivotOffset = new Vector3(0.69f,0.36f,0.0f);
    private Transform playerTransform;
    
    public Transform attackPoint;
    
    public LayerMask enemyLayers;

    
    private Animator animator;
    public Vector3 screenPosition;
    public Vector3 worldPosition;

    private Weapon weapon;
    private void Start()
    {
        weapon = new Roller(GREEN);
        playerTransform = transform.parent;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        screenPosition = Input.mousePosition;
        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        Vector3 direction = worldPosition - playerTransform.position;
        direction.z = 0;

        float distance = direction.magnitude;
        // Check if the position is outside the sphere.
        if (distance > weaponRange)
        {
            Vector3 closestPoint = playerTransform.position + direction.normalized * weaponRange;
            worldPosition = closestPoint;
        }
        if(distance < minWeaponRange){
            Vector3 closestPoint = playerTransform.position + direction.normalized * minWeaponRange;
            worldPosition = closestPoint;
        }
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward,direction);

        // Vector3 adjustedPosition = transform.position - rotation * pivotOffset;
        Vector3 adjustedPosition = worldPosition - rotation * (Vector3.up * 1.5f);

        while(animator.GetCurrentAnimatorStateInfo(0).shortNameHash != Animator.StringToHash("Roller_Swing")){
            transform.rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z);
            transform.position = adjustedPosition;

        }


    
        if(Input.GetButtonDown("Fire1")){
            Attack();
        }

    }

    public void Attack(){
        animator.SetTrigger("Attack");
        print("b4");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        print(hitEnemies.Length);
        foreach (Collider2D enemy in hitEnemies)
        {
            print("We hit "+ enemy.name);
        }
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
