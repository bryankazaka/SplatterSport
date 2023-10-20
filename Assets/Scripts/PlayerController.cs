using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int BLUE = 0, YELLOW = 1, GREEN = 2, PINK = 3;
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;

    public int colour;
    public int weapon;
    public int playerNum; //0 for player 1, 1 for player 2 ect
    public float speed;
    public float damageMult = 1.00f;
    public float attackSpeed;
    public float propagation = 1.00f;
    public float knockBack = 1.00f;
    public float stunTime = 1.00f;
    public bool leadLimbs = false;
    public bool isMouse = false;

    private PlayerMovement playerMovement;
    private WeaponController weaponController;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        gameObject.transform.parent =  GameObject.Find("GameManager").transform.Find("PlayersManager").transform;
        
    }

    void Start()
    {
        
        GetComponentInParent<GameManagerBattle>().addPlayer(playerNum,colour);
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        setPlayerColour(GREEN);
        setPlayerWeapon(ROLLER);
    }

    public void setPlayerColour(int colour)
    {
        this.colour = colour;
        var colours = new Color32[4];
        colours[BLUE] =  new Color32(0x00,0xB0,0xF6,0xFF); //blue
        colours[YELLOW] =  new Color32(0xFF,0xF3,0x00,0xFF); //yellow
        colours[GREEN] =  new Color32(0x15,0xFF,0x08,0xFF); //green
        colours[PINK] =  new Color32(0xFF,0x00,0x8E,0xFF); //pink
        spriteRenderer.color = colours[colour];
    }

    public void setPlayerWeapon(int weapon)
    {
        this.weapon = weapon;
    }

    public int getPlayerColour()
    {
        return this.colour;
    }

    public int getPlayerWeapon()
    {
        return this.weapon;
    }
}
