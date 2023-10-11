using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Collections;

public class CrowdController : MonoBehaviour
{
    public Tilemap crowdMap;
    public TileBase[] colors;
    private float[] crowd;
    // Start is called before the first frame update
    void Start()
    {
        crowd = new float[32];
        float[]  baseC = {1.0f,1.0f,1.0f,1.0f};
        updateCrowd(baseC);
        foreach (var item in crowd)
        {
            Debug.Log(item);
        }
        
    }
    
    public void updateCrowd(float[] ratio)
    {
        float total = ratio.Sum();
        for (int i = 0; i < 4; i++)
        {
            
            ratio[i] = math.round(32*(ratio[i]/total));
            Debug.Log(ratio[i]);
        }

        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < ratio[i];j++)
            {
                if (k < 32)
                {
                    crowd[k] = i +1;
                    k++;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 32; i++)
        {
            int xs = i - 16;
            crowdMap.SetTile(new Vector3Int(xs,5),colors[(int)crowd[i]-1]);
            crowdMap.SetTile(new Vector3Int(xs,6),colors[(int)crowd[i]-1]);
            crowdMap.SetTile(new Vector3Int(xs,7),colors[(int)crowd[i]-1]);
            
        }
            
        
    }

    
}
