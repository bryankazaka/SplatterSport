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
        float secondsPerUpdate = ((float)timerSeconds)/(32.0f*3.0f);

        state = 0;
        pos = (-16,9);
        InvokeRepeating(nameof(Light), 0, secondsPerUpdate); 
    }

    void Light()
    {
        state+=1;
        if (state > 3)
        {
            state = 1;
        }
        
        timerMap.SetTile(new Vector3Int(pos.Item1,pos.Item2,0),brazierStates[state]);
        if (state == 3)
        {
            pos.Item1 += 1;
        }
        
        if (pos.Item1 > 15)
        {
            GameObject.Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
