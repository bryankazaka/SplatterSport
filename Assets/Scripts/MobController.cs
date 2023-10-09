using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public float    speed;       //Speed of the Mob
    public float    limbSpeed;   //Speed of the Mobs Limbs
    public float    splatProp;   //The propagation strength of the Mob
    public float    health;      //The amount of hit points the mob has
    public bool[]   affects;      //Which affects alter the mob and its limbs {StickySplatter, Lead Limbs... ect} 
    private Vector3 origin; 
    private Vector3 dir;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = new Vector3(0,0,0);
          
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.Normalize(origin - transform.position);
        transform.position += dir * speed * 1/60;

        speed -= 0.01f;

        if (speed < 0)
        {
            GameObject.Destroy(gameObject);
        }

    }
}
