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
    private int[] playerColors;

    public AudioClip btn_highlight;
    public AudioClip btn_click;

    private AudioSource audioSource;

    private bool inBattle = false;

    private string[] playerModes = new string[] {"colour", "colour", "colour", "colour"};

    private Color32[] colors;

    public GameObject playerOneColour;
    public GameObject playerTwoColour;
    public GameObject playerThreeColour;
    public GameObject playerFourColour;

    public int playerOneCount;
    public int playerTwoCount;
    public int playerThreeCount;
    public int playerFourCount;

    void Start()
    {
        tRounds.text = "Rounds:" + "\n";
        tDrops.text = "Crowd Drops:" + "\n";
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.3f;  
        playerColors = new int[4];
        colors = new Color32[4];
        colors[0] =  new Color32(0x00,0xB0,0xF6,0xFF); //blue
        colors[1] =  new Color32(0xFF,0xF3,0x00,0xFF); //yellow
        colors[2] =  new Color32(0x15,0xFF,0x08,0xFF); //green
        colors[3] =  new Color32(0xFF,0x00,0x8E,0xFF); //pink
        playerOneCount = 0;
        playerTwoCount = 0;
        playerThreeCount = 0;
        playerFourCount = 0;
    }

    void Update()
    {
        if (!inBattle)
        {
           
            tRounds.text = "Rounds:" + "\n" + roundCounter[roundIndex];
            tDrops.text = "Crowd Drops:" + "\n" + dropChoice[dropIndex];
            
        }
        if (Input.GetKey(KeyCode.Space))
        {
            StartBattle();
        }

    }

    public void btnHighlight()
    {
        audioSource.PlayOneShot(btn_highlight, 6.0f);
    }

    public void btnClick()
    {
        audioSource.PlayOneShot(btn_click, 6.0f);
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

    public void StartBattle()
    {
        gameObject.GetComponentInChildren<MainSpawner>().enabled = true;
        gameObject.GetComponentInChildren<TimerController>().enabled = true;
        gameObject.GetComponentInChildren<PlayersManager>().StartGame();
       // gameObject.GetComponentInChildren<TimerController>().StartAgain();
       
        //reset the braziers
    }

    public void EndRound()
    {
        int winner = gameObject.GetComponentInChildren<SplatterController>().getWinner();
        gameObject.GetComponentInChildren<WallController>().winPoint(winner);
        gameObject.GetComponentInChildren<WallController>().UpdateWall(playerColors);
        gameObject.GetComponentInChildren<CrowdController>().CrowdReset();
        gameObject.GetComponentInChildren<TimerController>().StartAgain();
              
        //start the upgrade for losing players
        
       

        
    }
    public void addPlayer(int player,int color)
    {
        playerColors[player] = color;
    }


    public void dropsTextBackwards()
    {
        if(dropIndex !=0)
        {
            dropIndex--;
        }
    }

    /////////////////////// Button Transitions ////////////////////////
    
    public void playerOneRight()
    {
        if(playerOneCount==3)
        {
            playerOneCount = 0;
        }
        else
        {
            playerOneCount++;
        }
        if(playerModes[0].CompareTo("colour") == 0)
        {
            playerOneColour.gameObject.GetComponent<Image>().color = colors[playerOneCount];
        }
    }

    public void playerTwoRight()
    {
        if(playerTwoCount==3)
        {
            playerTwoCount = 0;
        }
        else
        {
            playerTwoCount++;
        }
        if(playerModes[1].CompareTo("colour") == 0)
        {
            playerTwoColour.gameObject.GetComponent<Image>().color = colors[playerTwoCount];
        }
    }

    public void playerThreeRight()
    {
        if(playerThreeCount==3)
        {
            playerThreeCount = 0;
        }
        else
        {
            playerThreeCount++;
        }
        if(playerModes[2].CompareTo("colour") == 0)
        {
            playerThreeColour.gameObject.GetComponent<Image>().color = colors[playerThreeCount];
        }
    }

    public void playerFourRight()
    {
        if(playerFourCount==3)
        {
            playerFourCount = 0;
        }
        else
        {
            playerFourCount++;
        }
        if(playerModes[3].CompareTo("colour") == 0)
        {
            playerFourColour.gameObject.GetComponent<Image>().color = colors[playerFourCount];
        }
    }

    // Now the Left methods:

    public void playerOneLeft()
    {
        if(playerOneCount==0)
        {
            playerOneCount = 3;
        }
        else
        {
            playerOneCount--;
        }
        if(playerModes[0].CompareTo("colour") == 0)
        {
            playerOneColour.gameObject.GetComponent<Image>().color = colors[playerOneCount];
        }
    }

    public void playerTwoLeft()
    {
        if(playerTwoCount==0)
        {
            playerTwoCount = 3;
        }
        else
        {
            playerTwoCount--;
        }
        if(playerModes[1].CompareTo("colour") == 0)
        {
            playerTwoColour.gameObject.GetComponent<Image>().color = colors[playerTwoCount];
        }
    }

    public void playerThreeLeft()
    {
        if(playerThreeCount==0)
        {
            playerThreeCount = 3;
        }
        else
        {
            playerThreeCount--;
        }
        if(playerModes[2].CompareTo("colour") == 0)
        {
            playerThreeColour.gameObject.GetComponent<Image>().color = colors[playerThreeCount];
        }
    }

    public void playerFourLeft()
    {
        if(playerFourCount==0)
        {
            playerFourCount = 3;
        }
        else
        {
            playerFourCount--;
        }
        if(playerModes[3].CompareTo("colour") == 0)
        {
            playerFourColour.gameObject.GetComponent<Image>().color = colors[playerFourCount];
        }
    }

}
