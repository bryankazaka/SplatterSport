using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimerController : MonoBehaviour
{
    public int timerSeconds;
    public TileBase[] brazierStates;
    private int state;
    private (int,int) pos;
    public Tilemap timerMap;
    // Start is called before the first frame update
    void Start()
    {
        float secondsPerUpdate = ((float)timerSeconds)/(32.0f*3.0f); //calculate the amount of time between brazier tile updates

        state = 0;
        pos = (-16,9); // the leftmost position of the braziers
        InvokeRepeating(nameof(Light), 0, secondsPerUpdate); //repeat the animation
    }

    void Light()
    {
        state+=1; // set it to the next state, cycles between 1, 2, and 3 to go between the three brazier states
        
        timerMap.SetTile(new Vector3Int(pos.Item1,pos.Item2,0),brazierStates[state]); //set the tile for the brazier
        
        if (state == 3) //if its the final state move to the next tile and reset the states
        {
            pos.Item1 += 1;
            state = 0;
        }
        
        if (pos.Item1 > 15) // when the timer runs out destroy the object {add end round function here}
        {
            GameObject.Destroy(gameObject);
        }
    }

}
