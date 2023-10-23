using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LimbScatter : MonoBehaviour
{
    private Vector3 direct; //direction of the limbs scattering
    private int spread; //spread in angles where the mob spreads to
    private int rotation; // the speed of the limbs rotation to simulate movement
    private float resistance = 1.0f; //how much speed the limb loses when bouncing
    private float speed; // the speed if the limbs movement
    private float splatProp; // the propagation strenght of the limb
    private int color; // the color of the player who killed the mob
    private Color32[] colors;
    public Tilemap splatterMap; // the map which the splatter is put on
    
    //function that is called to start the scattering process and get data from the parent mob
    public void Scatter(Vector3 dir, float speedP, int spreadP, int colorP,float splatPropP, float falloff) 
    {
        resistance *= falloff;
        spread = spreadP;
        transform.parent = null;
        speed = speedP;  
        color = colorP;  
        splatProp = splatPropP; 
        direct = Quaternion.AngleAxis(UnityEngine.Random.Range(-spread,spread), Vector3.back) * dir;
        direct.Normalize();
        rotation = UnityEngine.Random.Range(-10,10);
        colors = new Color32[4];
        colors[0] =  new Color32(0x00,0xB0,0xF6,0xFF);
        colors[1] =  new Color32(0xFF,0xF3,0x00,0xFF);
        colors[2] =  new Color32(0x15,0xFF,0x08,0xFF);
        colors[3] =  new Color32(0xFF,0x00,0x8E,0xFF);
        transform.parent =  GameObject.Find("Mobs").transform; 
        var SpriteRender = transform.GetComponent<SpriteRenderer>();
        SpriteRender.enabled = true;
        SpriteRender.color = colors[color];
    }

    void Update()
    {     
        var nextPos = transform.position + (speed * 1/60 * direct); //the next postion of the limb
        
        //checkes if the next position is out of bounds and if it is bounces the limb
        if (math.abs(nextPos.y + 3.5)>= 6.5f )
        {
            BounceY();
            speed -= resistance; 
        }
        else if (math.abs(nextPos.x) >= 16.0f)
        {
            BounceX(); 
            speed -= resistance; 
        }
        else
        {
            //move the limb
            direct.Normalize();            
            transform.position += speed * 1/60 * direct;
            transform.Rotate(Vector3.back,rotation);
        }
    
        //if the limb's speed reaches 0 or less call the splatter process and delete the limb object
        if (speed <= 0)
        {
        
            speed = 0;
            Vector3Int location = splatterMap.WorldToCell(transform.position);
            splatterMap.GetComponent<SplatterController>().Propagate(location,splatProp,color+1);
            GameObject.Destroy(gameObject);
        }
        else
        {
            speed -= 1/speed/3; //reduce the speed of the limb 
        }
        
       
    }

    //change the dir if hitting the ybounds
    void BounceY()
    {
        float angle;
        
            if (transform.position.y < 0)
            {
                angle = Vector3.Angle(direct,new Vector3(0,-1,0));
                
                if (direct.x < 0)
                {
                    
                    angle = 180 - 2*angle;
                }
                else
                {
                    angle = 180 + (angle*2);
                }
                
            
            }
            else
            {
                angle = Vector3.Angle(direct,new Vector3(0,1,0));
                if (direct.x < 0)
                {
                    
                    angle = 180 + 2*angle;
                }
                else
                {
                    angle = 180 - (angle*2);
                }
            
            }       
            direct = Quaternion.AngleAxis(angle, Vector3.back) * direct;
        
            direct.Normalize();        
    
    }
    //change the dir if hitting the xbounds
    void BounceX()
    {
        float angle;
       
            if (transform.position.x < 0)
            {
                angle = Vector3.Angle(direct,new Vector3(1,0,0));
                if (direct.y < 0)
                {
                    
                    angle = 180 - 2*angle;
                }
                else
                {
                    angle = 180 + (angle*2);
                }
                
            
            }
            else
            {
                angle = Vector3.Angle(direct,new Vector3(-1,0,0));
                if (direct.y < 0)
                {
                    
                    angle = 180 + 2*angle;
                }
                else
                {
                    angle = 180 - (angle*2);
                }
            
            }       
            direct = Quaternion.AngleAxis(angle, Vector3.back) * direct;
        
            direct.Normalize();
            
        
    
    }
}
