using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LimbScatter : MonoBehaviour
{
    private Vector3 direct;
    private int spread;
    private int rotation;
    private bool isScatter;
    private float speed;
    private float splatProp;
    private int color;
    public Tilemap splatterMap;
    
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
        isScatter = true;
        transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update()
    {
     if (isScatter)
        {
            var nextPos = transform.position + (speed * 1/60 * direct);
            
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
                direct.Normalize();
                
                transform.position += speed * 1/60 * direct;
                transform.Rotate(Vector3.back,rotation);
            }
        

            if (speed <= 0)
            {
                speed = 0;
                Vector3Int location = splatterMap.WorldToCell(transform.position);
                splatterMap.GetComponent<SplatterController>().Propagate(location,splatProp,color);
                GameObject.Destroy(gameObject);
            }
            else
            {
                speed -= 1/speed/4;
            }
            
        }
    }
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
