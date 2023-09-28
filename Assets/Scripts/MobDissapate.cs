using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.VisualScripting;

public class MobDissapate : MonoBehaviour
{
    public Transform[] limbs;
    public GameObject MainChar;
    public Vector3[] positions;
    private bool Scattered = false;
    // Start is called before the first frame update
    void Start()
    {
        limbs = new Transform[transform.childCount];
        int i = 0;
        foreach (Transform t in transform)
        {
            limbs[i++] = t;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
           Scattered = false;
           foreach (var limb in limbs)
                {
                    var limbScatter = limb.GetComponent<LimbScatter>();
                    limbScatter.isScatter = false;
                    limbScatter.speed  = 15 * UnityEngine.Random.Range(0.8f, 1.2f);
                }
           Rearange();

        }
        Vector3 direct = transform.position - MainChar.transform.position;
        
        if ((math.abs(transform.position.x - MainChar.transform.position.x) <= 1.0f) && 
        (math.abs(transform.position.y - MainChar.transform.position.y) <= 1.0f)){
            if (!Scattered)
            {
                Scattered = true;
                foreach (var limb in limbs)
                {
                    var limbScatter = limb.GetComponent<LimbScatter>();
                    limbScatter.Scatter(direct);
                    
                }
                GameObject.Destroy(gameObject);
        
            }
        }
      
    }

   
    void Rearange(){
        Vector3 targetPos;
        targetPos = transform.position;
        float step = 0.25f;
        int num = 0;
        targetPos.x = (-step *2)+ step/2;
        targetPos.y = (step * 2)- step/2;
        foreach (var limb in limbs)
        {   
            limb.position = targetPos;
            num++;
            if (num > 3)
            {
                num = 0;
                targetPos.x -=  3*step;
                targetPos.y -= step;
            }
            else
            {
                targetPos.x += step;
            }
        
        }
    }
   
}
