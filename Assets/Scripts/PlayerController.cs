using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    public LayerMask solidobjectsLayer;
    private void Update() 
    {
       
        if (!isMoving)
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
                
                
                targetPos = targetPos - transform.position;
                transform.position += targetPos * Time.deltaTime * moveSpeed;
           
                
            }
        }    
    }


  
}
