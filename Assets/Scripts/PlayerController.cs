using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int BLUE = 0, YELLOW = 1, RED = 2, GREEN = 3;
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;

    public int colour = BLUE;
    public int weapon = ROLLER;
    public float speed;
    public float damageMult = 1.00f;
    public float attackSpeed = 1.00f;
    public float propagation = 1.00f;
    public float knockBack = 1.00f;
    public float stunTime = 1.00f;
    public bool leadLimbs = false;

    private PlayerMovement playerMovement;
    private WeaponController weaponController;
    private SpriteRenderer spriteRenderer;

    // public PlayerController()
    // {
    //     //defaults
    //     this.colour = GREEN;
    //     this.weapon = BRUSH;
    //
    // }
    // public PlayerController(int colour, int weapon)
    // {
    //     this.colour = colour;
    //     this.weapon = weapon;
    // }
    void Start()
    {
        colour = GREEN;
        weapon = ROLLER;
    
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.green;
    }
    
}
