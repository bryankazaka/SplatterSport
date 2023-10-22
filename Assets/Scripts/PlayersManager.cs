using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayersManager : MonoBehaviour
{
    [SerializeField] private int numberOfPlayers = 0;
    public GameObject splattermap;
    public bool[] numbersTaken = {false,false,false,false};
    public bool[] colorsTaken = {false,false,false,false};
    // Start is called before the first frame update
    private string[] colors = {"blue","yellow","green","pink"};
   

    
    public void StartGame()
    {
        int[] colorPlayers = new int[4];
       
        foreach (Transform player in transform)
        {
            PlayerController playObj = player.GetComponent<PlayerController>();
            colorPlayers[playObj.colour] = playObj.playerNum;
            GetComponentInParent<GameManagerBattle>().updatePlayer(player.gameObject);
        }
        splattermap.GetComponent<SplatterController>().SetColor(colorPlayers);
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