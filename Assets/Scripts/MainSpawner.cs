using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform mob in transform) // Initiates the mobspawning script for each mob in gameObject Mobs
        {
            mob.GetComponent<MobSpawner>().Start();
            
        }
    }

}
