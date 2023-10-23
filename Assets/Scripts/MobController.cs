using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public float    speed;       //Speed of the Mob
    public float    limbSpeed;   //Speed of the Mobs Limbs
    public int    limbSpread;  //The spread of the Mobs Limbs
    public float    splatProp;   //The propagation strength of the Mob
    public float    health;      //The amount of hit points the mob has
   private float maxHealth;
    public bool[]   affects; //Which affects alter the mob and its limbs {StickySplatter, Lead Limbs... ect} 
    private Color32[] colors;
    private int color;     
    private Vector3 target; 
    private Vector3 dir;         //The Direction the Mob is moving in
    private float deAggro = 1;
    private Transform[] limbs;   //The limb objects of the mob
    public Transform players;
    private float knockBackSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
        limbs = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            limbs[i++] = t;
        }
        target = new Vector3(0,0,0);

        maxHealth = health;
        //randomize
        speed *= Random.Range(0.90f,1.10f);
        limbSpeed *= Random.Range(0.45f,0.55f);
        limbSpread = (int) (limbSpread * Random.Range(0.90f,1.10f));
        splatProp *= Random.Range(0.90f,1.10f);
        colors = new Color32[4];
        colors[0] =  new Color32(0x00,0xB0,0xF6,0xFF);
        colors[1] =  new Color32(0xFF,0xF3,0x00,0xFF);
        colors[2] =  new Color32(0x15,0xFF,0x08,0xFF);
        colors[3] =  new Color32(0xFF,0x00,0x8E,0xFF);
          
    }

    void knockBack(float strenght)
    {
        knockBackSpeed = strenght;
    }
    
    public void takeDamage(GameObject Player, float damage)
    {
        health -= damage;
        
        color = Player.GetComponent<PlayerController>().colour;
        var SpriteRender = transform.GetComponent<SpriteRenderer>();
        
        if (health <= 0)
        {
            Splatter();
        }
        knockBack(damage/5);
        var newCol =  colors[color];
        byte byteR = (byte)Mathf.Clamp(newCol.r - newCol.r*(health/maxHealth), 0f, 255f);
        byte byteG = (byte)Mathf.Clamp(newCol.g - newCol.g*(health/maxHealth), 0f, 255f);
        byte byteB = (byte)Mathf.Clamp(newCol.b - newCol.b*(health/maxHealth), 0f, 255f);
        SpriteRender.color = new Color32(byteR,byteG,byteB,255);
    }
    // Update is called once per frame
    void Splatter()
    {
        foreach (Transform limb in limbs)
            {
                limb.GetComponent<LimbScatter>().Scatter(-dir,limbSpeed,limbSpread,color,splatProp);
                limb.GetComponent<LimbScatter>().enabled = true;
                
            }
        GameObject.Destroy(gameObject);
    }
    void Update()
    {
        
        float dist = 100.0f;
        foreach (Transform player in players)
        {
            
            if (dist > (player.position - transform.position).magnitude)
            {
                dist = (player.position - transform.position).magnitude;
                deAggro = 1.0f;
                target = player.position;
                 if (player.gameObject.GetComponent<PlayerMovement>().isStunned && dist < 3)
                 {
                    deAggro = -0.1f;
                 }
                
            }
        }
        
        if (knockBackSpeed >= 0)
        {
            
            transform.position += -dir * knockBackSpeed * 1/60;
            knockBackSpeed -= 0.1f;
        }
        else
        {
            
            dir = deAggro * Vector3.Normalize(target - transform.position);
            
            transform.position += dir * speed * 1/60;
        }

    }
}
