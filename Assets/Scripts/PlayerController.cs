using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    const int BLUE = 0, YELLOW = 1, GREEN = 2, PINK = 3;
    private const int BRUSH = 0, PENCIL = 1, ROLLER = 2;


    public int colour = 0;
    public int weapon = 0;
    public int playerNum = 0; //0 for player 1, 1 for player 2 ect
    public float speed;
    public float damageMult = 1.00f;
    public float attackSpeed;
    public float propagation = 1.00f;
    public float knockBack = 1.00f;
    public float stunTime = 1.00f;
    public bool isMouse = false;

    private float[] upgrades; // StunT AtcSp Dmg Prop LmbSp Bounce KnckB Spread Size Rng Move

    public bool isDone = false;

    private PlayerMovement playerMovement;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        upgrades = new float[11];
        for (int i = 0; i < 11; i++)
        {
            upgrades[i] = 1.0f;
        }
        gameObject.transform.parent =  GameObject.Find("GameManager").transform.Find("PlayersManager").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void FinishChar()
    {
        isDone = true;
    }
    void Start()
    {        
        playerMovement = GetComponent<PlayerMovement>();
        
        playerNum = getFreeNum();
        GetComponentInParent<GameManagerBattle>().addPlayer(gameObject);
    }
    public void CreateWeapon(Sprite init)
    {
        foreach (Transform weaponT in transform)
        {
            weaponT.gameObject.SetActive(true);
            weaponT.gameObject.GetComponent<WeaponController>().initWeapon(colour,weapon,init);
        }
    }
    private int getFreeNum(int numP = -1)
    {
         bool[] num = GetComponentInParent<PlayersManager>().numbersTaken;
         numP += 1;
         if (!num[numP])
         {
            GetComponentInParent<PlayersManager>().numbersTaken[numP] = true;
            return numP;
         }
         else
         {
            return getFreeNum(numP);
         }
    }
    public void Upgrade(int type)
    {
        switch (type)
        {
            case 0:
                upgrades[0] = upgrades[0]*0.25f;
                break;
            case 1:
                upgrades[1] += 0.15f;
                upgrades[2] -= 0.05f;
                break;
            case 2:
                upgrades[3] += 0.1f;
                break;
            case 3:
                upgrades[4] += 0.1f;
                upgrades[5] *= 0.5f;
                break;
            case 4:
                upgrades[6] += 0.1f;
                upgrades[7] += 0.1f;
                break;
            case 5:
                upgrades[2] += 0.1f;
                break;
            case 6:
                upgrades[8] += 0.1f;
                upgrades[9] += 0.1f;
                break;
            case 7:
                upgrades[1] += 0.1f;
                break;
            case 8:
                upgrades[10] += 0.1f;
                break;
            case 9:
                upgrades[2] += 0.15f;
                upgrades[1] -= 0.1f;
                break;

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
    public void OnChooseL(InputAction.CallbackContext ctx)
    {   
       if (ctx.performed && !isDone)
        {    
                     
            GetComponentInParent<GameManagerBattle>().playerSelect(getPlayerNumber(),true);                   
        }  
    }

    public void OnChooseR(InputAction.CallbackContext ctx)
    {      
        if (ctx.performed && !isDone)
        {          
            
            GetComponentInParent<GameManagerBattle>().playerSelect(getPlayerNumber(),false);                   
        }           
    }

    public void OnSelect(InputAction.CallbackContext ctx)
    {
        
        if (ctx.performed && !isDone)
        {
           
            GetComponentInParent<GameManagerBattle>().PlayerNext(getPlayerNumber());
        }
       
        
        
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

    public int getPlayerNumber()
    {
        return playerNum;
    }
}
