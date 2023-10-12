using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LimbScatter : MonoBehaviour
{
    private Vector3 direct; //direction of the limbs scattering
    private int spread; //spread in angles where the mob spreads to
    private int rotation; // the speed of the limbs rotation to simulate movement
    private int resistance; //how much speed the limb loses when bouncing
    private float speed; // the speed if the limbs movement
    private float splatProp; // the propagation strenght of the limb
    private int color; // the color of the player who killed the mob
    public Tilemap splatterMap; // the map which the splatter is put on
    
    //function that is called to start the scattering process and get data from the parent mob
    public void Scatter(Vector3 dir, float speedP, int spreadP, int colorP,float splatPropP) 
    {
        spread = spreadP;
        transform.parent = null;
        speed = speedP;  
        color = colorP;  
        splatProp = splatPropP; 
        direct = Quaternion.AngleAxis(UnityEngine.Random.Range(-spread,spread), Vector3.back) * dir;
        direct.Normalize();
        rotation = UnityEngine.Random.Range(-10,10);
        
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {     
        var nextPos = transform.position + (speed * 1/60 * direct); //the next postion of the limb
        
        //checkes if the next position is out of bounds and if it is bounces the limb
        if (math.abs(nextPos.y + 3.5)>= 6.5f )
        {
            BounceY(); 
        }
        else if (math.abs(nextPos.x) >= 16.0f)
        {
            BounceX(); 
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
            splatterMap.GetComponent<SplatterController>().Propagate(location,splatProp,color);
            GameObject.Destroy(gameObject);
        }
        else
        {
            speed -= 1/speed/4; //reduce the speed of the limb 
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
