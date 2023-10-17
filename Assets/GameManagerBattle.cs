using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;  
using UnityEngine.SceneManagement;

public class GameManagerBattle : MonoBehaviour
{
    public TextMeshProUGUI tRounds;  
    public TextMeshProUGUI tDrops; 
    private string[] roundCounter = new string[] {"3","5","7"};
    private string[] dropChoice = new string[] {"Enabled", "Disabled"};
    private int roundIndex = 0;
    private int dropIndex = 0;

    private bool inBattle = false;

    void Start()
    {
        tRounds.text = "Rounds:" + "\n";
        tDrops.text = "Crowd Drops:" + "\n";
    }

    void Update()
    {
        if (!inBattle)
        {
           
            tRounds.text = "Rounds:" + "\n" + roundCounter[roundIndex];
            tDrops.text = "Crowd Drops:" + "\n" + dropChoice[dropIndex];
            if (Input.GetKeyDown(KeyCode.Return))
            {
                inBattle = true;
                startBattle();
                tRounds.ClearMesh();
                tDrops.ClearMesh();
            }
        }

    }

    public void roundsTextForward()
    {
        if(roundIndex !=2)
        {
            roundIndex++;
        }
    }

    public void roundsTextBackwards()
    {
        if(roundIndex !=0)
        {
            roundIndex--;
        }
    }

    public void dropsTextForward()
    {
        if(dropIndex !=1)
        {
            dropIndex++;
        }
    }

    public void startBattle()
    {
        gameObject.GetComponentInChildren<MainSpawner>().enabled = true;
        gameObject.GetComponentInChildren<PlayersController>().StartGame();
    }
    public void dropsTextBackwards()
    {
        if(dropIndex !=0)
        {
            dropIndex--;
        }
    }
}
