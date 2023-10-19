using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private int numberOfPlayers = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        
    }

    public void OnJoin(PlayerInput pi)
    {
        numberOfPlayers++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}