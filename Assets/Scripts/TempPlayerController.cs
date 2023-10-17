using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class TempPlayerController : MonoBehaviour
{
    public float moveSpeed;

   
    private Vector2 input;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input != Vector2.zero)
        {
            
            var targetPos = transform.position;
            
            if (math.abs(input.x) == math.abs(input.y))
            {
                targetPos.x += 1/math.sqrt(2) * input.x;
                targetPos.y += 1/math.sqrt(2) * input.y; 
            }
            else
            {
                targetPos.x += input.x;
                targetPos.y += input.y;
            }
            
            
            targetPos -= transform.position;
            transform.position += targetPos * 1/60 * moveSpeed;
            
            
        } 
        
    }
}
