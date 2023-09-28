using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobs : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mob;
    public GameObject mobClone;
    void Start()
    {
        Application.targetFrameRate = 60;
        InvokeRepeating(nameof(Spawn), 1.0f, 1.0f);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        mobClone = Instantiate(mob,position: new Vector3(Random.Range(-13,13),Random.Range(-3,3),0), mob.transform.rotation);
        mobClone.GetComponent<MobFollow>().enabled = true;
    
    }
}
