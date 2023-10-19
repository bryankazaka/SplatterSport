using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int BLUE = 0, YELLOW = 1, GREEN = 2, PINK = 3;
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;

    public int colour = BLUE;
    public int weapon = ROLLER;
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
        var colors = new Color32[4];
        colors[0] =  new Color32(0x00,0xB0,0xF6,0xFF); //blue
        colors[1] =  new Color32(0xFF,0xF3,0x00,0xFF); //yellow
        colors[2] =  new Color32(0x15,0xFF,0x08,0xFF); //green
        colors[3] =  new Color32(0xFF,0x00,0x8E,0xFF); //pink
        GetComponentInParent<GameManagerBattle>().addPlayer(playerNum,colour);
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[colour];
    }
    
}
