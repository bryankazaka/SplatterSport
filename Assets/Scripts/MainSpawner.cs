using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    public int numberOfPlayers;
    public int roundsPassed;

    // Start is called before the first frame update
    void Start()
    {
        roundsPassed = 0;
        Application.targetFrameRate = 60;
       
    }
    public void StartRound()
    {
        roundsPassed++;
         foreach (Transform mob in transform) // Initiates the mobspawning script for each mob in gameObject Mobs
         {
             MobSpawner spawner = mob.GetComponent<MobSpawner>();
             spawner.spawnRateMult = numberOfPlayers;
             spawner.roundMult = roundsPassed;
             spawner.StartRound();
             
         }
    }

    public void EndRound()
    {
        foreach (Transform mob in transform) // Initiates the mobspawning script for each mob in gameObject Mobs
        {
            mob.GetComponent<MobSpawner>().EndRound();
            
        }
    }

}
