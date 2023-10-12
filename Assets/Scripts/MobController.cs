using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MobController : MonoBehaviour
{

    public float    speed;       //Speed of the Mob
    public float    limbSpeed;   //Speed of the Mobs Limbs
    public int    limbSpread;  //The spread of the Mobs Limbs
    public float    splatProp;   //The propagation strength of the Mob
    public float    health;      //The amount of hit points the mob has
    public bool[]   affects;     //Which affects alter the mob and its limbs {StickySplatter, Lead Limbs... ect} 
    private Vector3 origin; 
    private Vector3 dir;         //The Direction the Mob is moving in
    private Transform[] limbs;   //The limb objects of the mob
    
    // Start is called before the first frame update
    void Start()
    {
        limbs = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            limbs[i++] = t;
        }
        origin = new Vector3(0,0,0);

        //randomize
        speed *= Random.Range(0.90f,1.10f);
        limbSpeed *= Random.Range(0.90f,1.10f);
        limbSpread = (int) (limbSpread * Random.Range(0.90f,1.10f));
        splatProp *= Random.Range(0.90f,1.10f);
          
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.Normalize(origin - transform.position);
        transform.position += dir * speed * 1/60;

        speed -= 0.01f;

        if (speed < 0)
        {
            int color = Random.Range(1,5);
            foreach (Transform limb in limbs)
            {
                
                limb.GetComponent<LimbScatter>().enabled = true;
                limb.GetComponent<LimbScatter>().Scatter(dir,limbSpeed,limbSpread,color,splatProp);
            }
            GameObject.Destroy(gameObject);
        }

    }
}
