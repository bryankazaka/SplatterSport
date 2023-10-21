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

    private int [] playerOneCharacter = new int[2];
    private int [] playerTwoCharacter = new int[2];
    private int [] playerThreeCharacter = new int[2];
    private int [] playerFourCharacter = new int[2];

    private Color32[] colors;

    public GameObject playerOneColour;
    public GameObject playerTwoColour;
    public GameObject playerThreeColour;
    public GameObject playerFourColour;



    private int playerOneCount;
    private int playerTwoCount;
    private int playerThreeCount;
    private int playerFourCount;

    public Sprite [] weapons;

    public TextMeshProUGUI playerOneNextButtonText;
    public TextMeshProUGUI playerTwoNextButtonText;
    public TextMeshProUGUI playerThreeNextButtonText;
    public TextMeshProUGUI playerFourNextButtonText;

    void Start()
    {
        tRounds.text = "Rounds:" + "\n";
        tDrops.text = "Crowd Drops:" + "\n";
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.3f;  
        playerColors = new int[4];
        colors = new Color32[5];
        colors[0] =  new Color32(0x00,0xB0,0xF6,0xFF); //blue
        colors[1] =  new Color32(0xFF,0xF3,0x00,0xFF); //yellow
        colors[2] =  new Color32(0x15,0xFF,0x08,0xFF); //green
        colors[3] =  new Color32(0xFF,0x00,0x8E,0xFF); //pink
        colors[4] =  new Color32(0xFF,0xFF,0xFF,0xFF); //white
        playerOneColour.gameObject.GetComponent<Image>().color = colors[0];
        playerTwoColour.gameObject.GetComponent<Image>().color = colors[0];
        playerThreeColour.gameObject.GetComponent<Image>().color = colors[0];
        playerFourColour.gameObject.GetComponent<Image>().color = colors[0];
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
        GetComponentInChildren<MainSpawner>().enabled = true;
        GetComponentInChildren<TimerController>().enabled = true;
        GetComponentInChildren<PlayersManager>().StartGame();
       // gameObject.GetComponentInChildren<TimerController>().StartAgain();
       
        //reset the braziers
    }

    public void EndRound()
    {
        int winner = GetComponentInChildren<SplatterController>().getWinner();
        GetComponentInChildren<WallController>().winPoint(winner);
        GetComponentInChildren<WallController>().UpdateWall(playerColors);
        GetComponentInChildren<CrowdController>().CrowdReset();
        GetComponentInChildren<MainSpawner>().EndRound();
        GetComponentInChildren<MobsManager>().EndRound();
        
              
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
        
        if(playerModes[0].CompareTo("colour") == 0)
        {
            if(playerOneCount==3)
            {
                playerOneCount = 0;
            }
            else
            {
                playerOneCount++;
            }
            playerOneColour.gameObject.GetComponent<Image>().color = colors[playerOneCount];
        }
        if(playerModes[0].CompareTo("weapon") == 0)
        {
            if(playerOneCount + 3 > 11)
            {
                playerOneCount = playerOneCount % 3;
            }
            else
            {
                playerOneCount += 3;
            }
            
            playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];


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

    public void playerOneNext()
    {
        if(playerModes[0].CompareTo("colour") == 0)
        {
            playerModes[0] = "weapon";
            playerOneNextButtonText.text = "Done";
            switch(playerOneCount)
            {
                case 0:
                    playerOneCount = 0;
                    break;
                case 1:
                    playerOneCount = 3;
                    break;
                case 2:
                    playerOneCount = 6;
                    break;
                case 3:
                    playerOneCount = 9;
                    break;
            }
            playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];
            playerOneColour.gameObject.GetComponent<Image>().color = colors[4];

        }
    }

    public void playerTwoNext()
    {
        if (playerModes[1].CompareTo("colour") == 0)
        {
            playerModes[1] = "weapon";
            playerTwoNextButtonText.text = "Done";
            switch (playerTwoCount)
            {
                case 0:
                    playerTwoCount = 0;
                    break;
                case 1:
                    playerTwoCount = 3;
                    break;
                case 2:
                    playerTwoCount = 6;
                    break;
                case 3:
                    playerTwoCount = 9;
                    break;
            }
            playerTwoColour.gameObject.GetComponent<Image>().sprite = weapons[playerTwoCount];
            playerTwoColour.gameObject.GetComponent<Image>().color = colors[4];
        }
    }

    public void playerThreeNext()
    {
        if (playerModes[2].CompareTo("colour") == 0)
        {
            playerModes[2] = "weapon";
            playerThreeNextButtonText.text = "Done";
            switch (playerThreeCount)
            {
                case 0:
                    playerThreeCount = 0;
                    break;
                case 1:
                    playerThreeCount = 3;
                    break;
                case 2:
                    playerThreeCount = 6;
                    break;
                case 3:
                    playerThreeCount = 9;
                    break;
            }
            playerThreeColour.gameObject.GetComponent<Image>().sprite = weapons[playerThreeCount];
            playerThreeColour.gameObject.GetComponent<Image>().color = colors[4];
        }
    }

    public void playerFourNext()
    {
        if (playerModes[3].CompareTo("colour") == 0)
        {
            playerModes[3] = "weapon";
            playerFourNextButtonText.text = "Done";
            switch (playerFourCount)
            {
                case 0:
                    playerFourCount = 0;
                    break;
                case 1:
                    playerFourCount = 3;
                    break;
                case 2:
                    playerFourCount = 6;
                    break;
                case 3:
                    playerFourCount = 9;
                    break;
            }
            playerFourColour.gameObject.GetComponent<Image>().sprite = weapons[playerFourCount];
            playerFourColour.gameObject.GetComponent<Image>().color = colors[4];
        }
    }


}
