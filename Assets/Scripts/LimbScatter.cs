using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;
public class LimbScatter : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isScatter;
    public Vector3 direct;
    public bool bounced;

    public int spread;

    public float speed;  

    public Tilemap splatterMap;
    public TileBase splatter;

    public int rotation;


    void Start()
    {
        isScatter = false;
        speed = 12;
        spread = 30;         
    }

    public void Scatter(Vector3 dir)
    {
        transform.parent = null;
        
        direct = Quaternion.AngleAxis(UnityEngine.Random.Range(-spread,spread), Vector3.back) * dir;
        direct.Normalize();
        rotation = UnityEngine.Random.Range(-10,10);
        isScatter = true;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (isScatter)
        {
            if (speed > 12)
            {
                Debug.Log(speed);
            }
            if (math.abs((transform.position + (speed * Time.deltaTime * direct)).y) >= 4.0f)
            {
                BounceY(); 
            }
            else if (math.abs((transform.position + (speed * Time.deltaTime * direct)).x) >= 14.0f)
            {
                BounceX(); 
            }
            else
            {
                direct.Normalize();
                
                transform.position += speed * 0.015f * direct;
                transform.Rotate(Vector3.back,rotation);
            }
        

            if (speed <= 0)
            {
                speed = 0;
                Splatter();
                GameObject.Destroy(gameObject);
            }
            else
            {
                speed -= 1/speed;
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
            bounced = true;
        
    
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
            bounced = true;
        
    
    }
    void Splatter()
    {
        Vector3Int location = splatterMap.WorldToCell(transform.position);
        
        Propogate(1.0f,location);
    }
    void Propogate(float stren, Vector3Int loca)
    {
        
        if ((stren >= UnityEngine.Random.Range(0.0f,1.0f)) 
        && loca.y < 40 && loca.y > -41 && loca.x < 140 && loca.x > -141)
        {
            splatterMap.SetTile(loca,splatter);
            loca.y += 1;
            Propogate(stren - 0.1f, loca);
            loca.y -= 2;
            Propogate(stren - 0.1f, loca);
            loca.y +=1;
            loca.x +=1;
            Propogate(stren - 0.1f, loca);
            loca.x -= 2;
            Propogate(stren - 0.1f, loca);           

        }
        

    }
    
}
