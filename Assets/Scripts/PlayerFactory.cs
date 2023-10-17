using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    public int noPlayers = 2;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        switch (noPlayers)
        {
            case 2:
                player1 = playerPrefab;
                Instantiate(player1, new Vector3(9f, 0f, 0f), Quaternion.identity);
                // player2 = playerPrefab;
                // Instantiate(player2, new Vector3(-9f, -14f, 0f), Quaternion.identity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
