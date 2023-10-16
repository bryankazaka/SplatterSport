using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimerController : MonoBehaviour
{
    public int timerSeconds;
    public TileBase[] brazierStates;
    private Vector3Int pos;
    public Tilemap timerMap;
    // Start is called before the first frame update
    void Start()
    {
        float secondsPerUpdate = ((float)timerSeconds)/(26.0f); //calculate the amount of time between brazier tile updates

       
        pos = new Vector3Int(12,8,0); // the leftmost position of the braziers
        for (int i = 0; i < 26; i++)
        {
            pos.x -= 1;
            timerMap.SetAnimationFrame(pos,Random.Range(0,9));
            
        }
        pos = new Vector3Int(12,8,0);
        InvokeRepeating(nameof(Light), 0, secondsPerUpdate); //repeat the animation
    }

    void Light()
    {
       
        
        timerMap.SetTile(pos,brazierStates[1]); //set the tile for the brazier
        
        pos.x -= 1;
        
        if (pos.x < -13) // when the timer runs out destroy the object {add end round function here}
        {
            GameObject.Destroy(gameObject);
        }
    }

}
