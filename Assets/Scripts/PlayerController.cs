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
    public int playerNum = 0; //0 for player 1, 1 for player 2 ect
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
        setPlayerWeapon(ROLLER);
        spriteRenderer = GetComponent<SpriteRenderer>();
        setPlayerColour(getFreeCol());
    }

    void Start()
    {        
        playerMovement = GetComponent<PlayerMovement>();
        weaponController = GetComponentInChildren<WeaponController>();
        
        
        playerNum = getFreeNum();
        
       
        Debug.Log(colour+ " | "+weapon);
        GetComponentInParent<GameManagerBattle>().addPlayer(playerNum,colour);
    }

    private int getFreeNum()
    {
         bool[] num = GetComponentInParent<PlayersManager>().numbersTaken;
         var randomT = UnityEngine.Random.Range(0,4);
         if (!num[randomT])
         {
            GetComponentInParent<PlayersManager>().numbersTaken[randomT] = true;
            return randomT;
         }
         else
         {
            return getFreeNum();
         }
    }

    private int getFreeCol()
    {
         bool[] num = GetComponentInParent<PlayersManager>().colorsTaken;
         var randomT = UnityEngine.Random.Range(0,4);
         if (!num[randomT])
         {
            GetComponentInParent<PlayersManager>().colorsTaken[randomT] = true;
            return randomT;
         }
         else
         {
            return getFreeCol();
         }
    }
    public void setPlayerColour(int colourNew)
    {
        colour = colourNew;
        var colours = new Color32[4];
        colours[BLUE] =  new Color32(0x00,0xB0,0xF6,0xFF); //blue
        colours[YELLOW] =  new Color32(0xFF,0xF3,0x00,0xFF); //yellow
        colours[GREEN] =  new Color32(0x15,0xFF,0x08,0xFF); //green
        colours[PINK] =  new Color32(0xFF,0x00,0x8E,0xFF); //pink
        spriteRenderer.color = colours[colourNew];
    }

    public void setPlayerWeapon(int weaponNew)
    {
        weapon = weaponNew;
    }

    public int getPlayerColour()
    {
        return colour;
    }

    public int getPlayerWeapon()
    {
        return weapon;
    }
}
