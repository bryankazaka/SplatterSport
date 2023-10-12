using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public float mobsPerSecond; //how many mobs spawn per second
    public float spawnWaitTime; //how long before mobs being to spawn 

    // Start is called before the first frame update
    public void Start()
    {
        mobsPerSecond = 1.0f/mobsPerSecond; 
        InvokeRepeating(nameof(Spawn), spawnWaitTime, mobsPerSecond); //repeats the instantiation of another mob object
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn() //spawns a mob and enables the rendering and controller
    {        
        Transform mobClone = Instantiate(transform,position: SpawnPoint(), transform.rotation);
        mobClone.GetComponent<MobController>().enabled = true;
        mobClone.GetComponent<SpriteRenderer>().enabled = true;
       
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
