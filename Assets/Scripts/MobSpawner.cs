using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{

    public float spawnFreq;
    public float spawnWaitTime;

    // Start is called before the first frame update
    public void Start()
    {
        InvokeRepeating(nameof(Spawn), spawnWaitTime, spawnFreq); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {        
        Transform mobClone = Instantiate(transform,position: SpawnPoint(), transform.rotation);
        mobClone.GetComponent<MobController>().enabled = true;
        mobClone.GetComponent<SpriteRenderer>().enabled = true;
       
    }
    

    Vector3 SpawnPoint()
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
