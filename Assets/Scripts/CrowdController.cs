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
    public Tilemap crowdBMap; //tilemap of the crowd
    public Color32[] colors; // the differenc colors of crowds

    public Tilemap crowdHMap;
    public AnimatedTile[] heads;
    private float[] crowd; //data representation of the crowd
    // Start is called before the first frame update
    void Start()
    {
        crowd = new float[42];
        float[]  baseC = {1.0f,1.0f,1.0f,1.0f};
        updateCrowd(baseC);
        for (int i = 0; i < 42; i++)
        {
            //change the crowd ratios and set the tiles
            int xs = i - 21;
            crowdHMap.SetTile(new Vector3Int(xs,6),heads[UnityEngine.Random.Range(0,4)]);
            crowdHMap.SetAnimationTime(new Vector3Int(xs,6),UnityEngine.Random.Range(0,4));
            crowdBMap.SetAnimationTime(new Vector3Int(xs,6),UnityEngine.Random.Range(0,4));
            crowdHMap.SetTile(new Vector3Int(xs,7),heads[UnityEngine.Random.Range(0,4)]);
            crowdHMap.SetAnimationTime(new Vector3Int(xs,7),UnityEngine.Random.Range(0,4));
            crowdBMap.SetAnimationTime(new Vector3Int(xs,7),UnityEngine.Random.Range(0,4));
            crowdHMap.SetTile(new Vector3Int(xs,8),heads[UnityEngine.Random.Range(0,4)]);
            crowdHMap.SetAnimationTime(new Vector3Int(xs,8),UnityEngine.Random.Range(0,4));
            crowdBMap.SetAnimationTime(new Vector3Int(xs,8),UnityEngine.Random.Range(0,4));
            crowdHMap.SetTile(new Vector3Int(xs,9),heads[UnityEngine.Random.Range(0,4)]);
            crowdHMap.SetAnimationTime(new Vector3Int(xs,9),UnityEngine.Random.Range(0,4));
            crowdBMap.SetAnimationTime(new Vector3Int(xs,9),UnityEngine.Random.Range(0,4));
           
            
        }
    }
    
    public void updateCrowd(float[] ratio)
    {
        float total = ratio.Sum();
        for (int i = 0; i < 4; i++)
        {
            
            ratio[i] = math.round(42*(ratio[i]/total)); //finds the ratio of the colors
            
        }

        int k = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < ratio[i];j++)
            {
                if (k < 42)
                {
                    crowd[k] = i; //set the crowd to the ratio
                    k++;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 42; i++)
        {
            //change the crowd ratios and set the tiles
            int xs = i - 21;
            crowdBMap.SetColor(new Vector3Int(xs,6),colors[(int)crowd[i]]);
            crowdBMap.SetColor(new Vector3Int(xs,7),colors[(int)crowd[i]]);
            crowdBMap.SetColor(new Vector3Int(xs,8),colors[(int)crowd[i]]);
            crowdBMap.SetColor(new Vector3Int(xs,9),colors[(int)crowd[i]]);
           
            
        }
            
        
    }

    
}
