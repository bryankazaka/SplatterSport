using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;

    public int speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.Normalize(Player.transform.position - transform.position) * speed * Time.deltaTime;
    }
}
