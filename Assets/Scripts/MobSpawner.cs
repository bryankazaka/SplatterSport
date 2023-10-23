using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MobSpawner : MonoBehaviour
{

    public float mobsPerSecond; //how many mobs spawn per second
    public float spawnWaitTime; //how long before mobs being to spawn 

    public float spawnRateMult;

    public float mobBaseSpawnUnit;

    public int roundMult;

    // Start is called before the first frame update
    private void Awake()
    {
        
        switch (gameObject.name)
        {
            case "bucketM":
                mobBaseSpawnUnit = 0.3f;
                break;
            case "canvasM":
                mobBaseSpawnUnit = 0.1f;
                break;
            case "statueM":
                mobBaseSpawnUnit = 0.025f;
                break;
        }
    }

    public void StartRound()
    {
        mobsPerSecond = mobBaseSpawnUnit * spawnRateMult * ((roundMult/10)+1.0f);
        float newmobsPerSecond = 1.0f/mobsPerSecond; 
        InvokeRepeating(nameof(Spawn), spawnWaitTime, newmobsPerSecond); //repeats the instantiation of another mob object
    }

    
    public void EndRound()
    {
        
        CancelInvoke(nameof(Spawn));
    }

    void Spawn() //spawns a mob and enables the rendering and controller
    {        
        Transform mobClone = Instantiate(transform,position: SpawnPoint(), transform.rotation);
        mobClone.GetComponent<MobController>().enabled = true;
        mobClone.GetComponent<SpriteRenderer>().enabled = true;
        mobClone.parent =  GameObject.Find("GameManager").transform.Find("Mobs").transform; 
       
    }
    

    Vector3 SpawnPoint() // randomly generates a spawnpoint within the bounds of the 3 outside screens.
    {
        int side = Random.Range(1,4);

        return side switch
        {
            1 => new Vector3(-16.25f, Random.Range(2.75f,-10.25f), 0),
            2 => new Vector3(Random.Range(16.25f, -16.25f), -10.25f, 0),
            3 => new Vector3(16.25f, Random.Range(2.75f, -10.25f), 0),
            _ => new Vector3(0, 0, 0),
        };
    }
}
