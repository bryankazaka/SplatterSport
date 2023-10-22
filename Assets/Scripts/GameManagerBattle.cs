using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;  
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Player
{
    public int colour;
    public int weapon;

    public Player(int colour, int weapon)
    {
        this.colour = colour;
        this.weapon = weapon;
    }
}

public class GameManagerBattle : MonoBehaviour
{
    public TextMeshProUGUI tRounds;  
    public TextMeshProUGUI tDrops; 
    private string[] roundCounter = new string[] {"3","5","7"};
    private string[] dropChoice = new string[] {"Enabled", "Disabled"};
    private int roundIndex = 0;
    private int dropIndex = 0;
    private int[] playerColors;
    private GameObject[] players;

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
    private string[] colorsT = {"blue","yellow","green","pink"};

    public Button playerOneButton;
    public Button playerTwoButton;
    public Button playerThreeButton;
    public Button playerFourButton;

    public TextMeshProUGUI playerOneNextButtonText;
    public TextMeshProUGUI playerTwoNextButtonText;
    public TextMeshProUGUI playerThreeNextButtonText;
    public TextMeshProUGUI playerFourNextButtonText;

    public GameObject charSelect;
    private List<int> takenColours;

    private Player playerOne;
    private Player playerTwo;
    private Player playerThree;
    private Player playerFour;

    public GameObject losersPanel; 
    public int winner;

    public Sprite [] upgrades;

    void Start()
    {
        tRounds.text = "Rounds:" + "\n";
        tDrops.text = "Crowd Drops:" + "\n";
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.3f;  
        playerColors = new int[4];
        players = new GameObject[4];
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
        takenColours = new List<int> {};
    }

    void Awake()
    {
        StartCoroutine(turnOneCharSelect());
    }


    IEnumerator turnOneCharSelect()
    {
        yield return new WaitForSeconds(0.4f);
        charSelect.SetActive(true);
    }

    void Update()
    {
        
        tRounds.text = "Rounds:" + "\n" + roundCounter[roundIndex];
        tDrops.text = "Crowd Drops:" + "\n" + dropChoice[dropIndex];
            
        
        if (Input.GetKey(KeyCode.Space) && !inBattle)
        {
            StartBattle();
            inBattle = true;
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
    public void playerSelect(int playerNum, bool left)
    {
        
        switch (playerNum)
        {
            case 0:
                if (left)
                {
                    playerOneLeft();
                }
                else
                {
                    playerOneRight();
                }
                break;
            case 1:
                if (left)
                {
                    playerTwoLeft();
                }
                else
                {   
                    playerTwoRight();
                }
                break;
            case 2:
                if (left)
                {
                    playerThreeLeft();
                }
                else
                {
                    playerThreeRight();
                }
                break;
            case 3:
                if (left)
                {
                    playerFourLeft();
                }
                else
                {
                    playerFourRight();
                }
                break; 
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
        GetComponentInChildren<MainSpawner>().StartRound();
        GetComponentInChildren<TimerController>().enabled = true;
        GetComponentInChildren<PlayersManager>().StartGame();
        GetComponentInChildren<TimerController>().StartAgain();
        
       
        //reset the braziers
    }
    


    public void EndRound()
    {
        winner = GetComponentInChildren<SplatterController>().getWinner();
        GetComponentInChildren<WallController>().winPoint(winner);
        GetComponentInChildren<WallController>().UpdateWall(playerColors);
        GetComponentInChildren<CrowdController>().CrowdReset();
        GetComponentInChildren<MainSpawner>().EndRound();
        GetComponentInChildren<MobsManager>().EndRound();
        inBattle = false;
        //start the upgrade for losing players       
    }
    public void addPlayer(GameObject player)
    {
        PlayerController play = player.GetComponent<PlayerController>();
        players[play.playerNum] = player;
        playerColors[play.playerNum] = play.getPlayerColour(); // [playernum] = color
        
        switch (play.playerNum)
        {            
           case 0:
            GameObject.Find("pOneCircle").GetComponent<Button>().onClick.Invoke();
           break;
           case 1:
            GameObject.Find("pTwoCircle").GetComponent<Button>().onClick.Invoke();
           break;
           case 2:
            GameObject.Find("pThreeCircle").GetComponent<Button>().onClick.Invoke();
           break;
           case 3:
            GameObject.Find("pFourCircle").GetComponent<Button>().onClick.Invoke();
           break;

        }
    }

     public void updatePlayer(GameObject player)
     {        
        PlayerController play = player.GetComponent<PlayerController>();
        players[play.playerNum] = player;
        playerColors[play.playerNum] = play.getPlayerColour(); // [playernum] = color
       
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
            players[0].GetComponent<PlayerController>().setPlayerColour(playerOneCount);
        }
        if(playerModes[0].CompareTo("weapon") == 0)
        {
            int breakzone = playerOneCount + 4;
            if(breakzone > 11)
            {
                playerOneCount = playerOneCount % 4;
            }
            else
            {
                playerOneCount += 4;
            }
            playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];
            int tempNum = (playerOneCount - playerOneCount%4)/4;
            players[0].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[0].GetComponentInChildren<WeaponController>().initWeapon(
                players[0].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void playerTwoRight()
    {
        if (playerModes[1].CompareTo("colour") == 0)
        {
            if(playerTwoCount == 3)
            {
                playerTwoCount = 0;
            }
            else
            {
                playerTwoCount++;
            }
            playerTwoColour.gameObject.GetComponent<Image>().color = colors[playerTwoCount];
            players[1].GetComponent<PlayerController>().setPlayerColour(playerTwoCount);
        }
        else if (playerModes[1].CompareTo("weapon") == 0)
        {
            int breakzone = playerTwoCount + 4;
            if(breakzone > 11)
            {
                playerTwoCount = playerTwoCount % 4;
            }
            else
            {
                playerTwoCount += 4;
            }
            playerTwoColour.gameObject.GetComponent<Image>().sprite = weapons[playerTwoCount];
            int tempNum = (playerTwoCount - playerTwoCount%4)/4;
            players[1].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[1].GetComponentInChildren<WeaponController>().initWeapon(
                players[1].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void playerThreeRight()
    {
        if (playerModes[2].CompareTo("colour") == 0)
        {
            if(playerThreeCount == 3)
            {
                playerThreeCount = 0;
            }
            else
            {
                playerThreeCount++;
            }
            playerThreeColour.gameObject.GetComponent<Image>().color = colors[playerThreeCount];
            players[2].GetComponent<PlayerController>().setPlayerColour(playerThreeCount);
        }
        else if (playerModes[2].CompareTo("weapon") == 0)
        {
            int breakzone = playerThreeCount + 4;
            if(breakzone > 11)
            {
                playerThreeCount = playerThreeCount % 4;
            }
            else
            {
                playerThreeCount += 4;
            }
            playerThreeColour.gameObject.GetComponent<Image>().sprite = weapons[playerThreeCount];
            int tempNum = (playerThreeCount - playerThreeCount%4)/4;
            players[2].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[2].GetComponentInChildren<WeaponController>().initWeapon(
                players[2].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void playerFourRight()
    {
        if (playerModes[3].CompareTo("colour") == 0)
        {
            if(playerFourCount == 3)
            {
                playerFourCount = 0;
            }
            else
            {
                playerFourCount++;
            }
            playerFourColour.gameObject.GetComponent<Image>().color = colors[playerFourCount];
            players[3].GetComponent<PlayerController>().setPlayerColour(playerFourCount);
        }
        else if (playerModes[3].CompareTo("weapon") == 0)
        {
            int breakzone = playerFourCount + 4;
            if(breakzone > 11)
            {
                playerFourCount = playerFourCount % 4;
            }
            else
            {
                playerFourCount += 4;
            }
            playerFourColour.gameObject.GetComponent<Image>().sprite = weapons[playerFourCount];
            int tempNum = (playerFourCount - playerFourCount%4)/4;
            players[3].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[3].GetComponentInChildren<WeaponController>().initWeapon(
                players[3].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }


    // Now the Left methods:

    public void playerOneLeft()
    {
        if (playerModes[0].CompareTo("colour") == 0)
        {
            if(playerOneCount == 0)
            {
                playerOneCount = 3;
            }
            else
            {
                playerOneCount--;
            }
            playerOneColour.gameObject.GetComponent<Image>().color = colors[playerOneCount];
            players[0].GetComponent<PlayerController>().setPlayerColour(playerOneCount);
        }
        else if (playerModes[0].CompareTo("weapon") == 0)
        {
            int breakzone = playerOneCount - 4;
            if(breakzone < 0)
            {
                playerOneCount = 12 + breakzone;
            }
            else
            {
                playerOneCount -= 4;
            }
            playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];
            int tempNum = (playerOneCount - playerOneCount%4)/4;
            players[1].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[1].GetComponentInChildren<WeaponController>().initWeapon(
                players[1].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    // Similar pattern for the rest:

    public void playerTwoLeft()
    {
        if (playerModes[1].CompareTo("colour") == 0)
        {
            if(playerTwoCount == 0)
            {
                playerTwoCount = 3;
            }
            else
            {
                playerTwoCount--;
            }
            playerTwoColour.gameObject.GetComponent<Image>().color = colors[playerTwoCount];
            players[1].GetComponent<PlayerController>().setPlayerColour(playerTwoCount);
        }
        else if (playerModes[1].CompareTo("weapon") == 0)
        {
            int breakzone = playerTwoCount - 4;
            if(breakzone < 0)
            {
                playerTwoCount = 12 + breakzone;
            }
            else
            {
                playerTwoCount -= 4;
            }
            playerTwoColour.gameObject.GetComponent<Image>().sprite = weapons[playerTwoCount];
            int tempNum = (playerTwoCount - playerTwoCount%4)/4;
            players[1].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[1].GetComponentInChildren<WeaponController>().initWeapon(
                players[1].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void playerThreeLeft()
    {
        if (playerModes[2].CompareTo("colour") == 0)
        {
            if(playerThreeCount == 0)
            {
                playerThreeCount = 3;
            }
            else
            {
                playerThreeCount--;
            }
            playerThreeColour.gameObject.GetComponent<Image>().color = colors[playerThreeCount];
            players[2].GetComponent<PlayerController>().setPlayerColour(playerThreeCount);
        }
        else if (playerModes[2].CompareTo("weapon") == 0)
        {
            int breakzone = playerThreeCount - 4;
            if(breakzone < 0)
            {
                playerThreeCount = 12 + breakzone;
            }
            else
            {
                playerThreeCount -= 4;
            }
            playerThreeColour.gameObject.GetComponent<Image>().sprite = weapons[playerThreeCount];
            int tempNum = (playerThreeCount - playerThreeCount%4)/4;
            players[2].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[2].GetComponentInChildren<WeaponController>().initWeapon(
                players[2].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void playerFourLeft()
    {
        if (playerModes[3].CompareTo("colour") == 0)
        {
            if(playerFourCount == 0)
            {
                playerFourCount = 3;
            }
            else
            {
                playerFourCount--;
            }
            playerFourColour.gameObject.GetComponent<Image>().color = colors[playerFourCount];
            players[3].GetComponent<PlayerController>().setPlayerColour(playerFourCount);
        }
        else if (playerModes[3].CompareTo("weapon") == 0)
        {
            int breakzone = playerFourCount - 4;
            if(breakzone < 0)
            {
                playerFourCount = 12 + breakzone;
            }
            else
            {
                playerFourCount -= 4;
            }
            playerFourColour.gameObject.GetComponent<Image>().sprite = weapons[playerFourCount];
            int tempNum = (playerFourCount - playerFourCount%4)/4;
            players[3].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
            players[3].GetComponentInChildren<WeaponController>().initWeapon(
                players[3].GetComponent<PlayerController>().getPlayerColour(),tempNum);
        }
    }

    public void PlayerNext(int playerNum)
    {
        switch (playerNum)
        {            
           case 0:
            playerOneNext();
           break;
           case 1:
            playerTwoNext();
           break;
           case 2:
            playerThreeNext();
           break;
           case 3:
            playerFourNext();
           break;

        }
    }

    public void playerOneNext()
    {
        if (!takenColours.Contains(playerOneCount))
        {
            if(playerModes[0].CompareTo("colour") == 0)
            {
                playerModes[0] = "weapon";
                playerOneNextButtonText.text = "Done";
                playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];
                takenColours.Add(playerOneCount);
                playerOneButton.gameObject.SetActive(false);
            }
        }
        /*
        if(playerModes[0].CompareTo("weapon") == 0)
        {
            playerOne = new Player(playerOneCount % 4, playerOneCount);
        }*/
         
    }

    public void playerTwoNext()
    {
        if (!takenColours.Contains(playerTwoCount))
        {
            if (playerModes[1].CompareTo("colour") == 0)
            {
                playerModes[1] = "weapon";
                playerTwoNextButtonText.text = "Done";
                playerTwoColour.gameObject.GetComponent<Image>().sprite = weapons[playerTwoCount];
                takenColours.Add(playerTwoCount);
                playerTwoButton.gameObject.SetActive(false);
            }
        }
        /*if(playerModes[1].CompareTo("weapon") == 0)
        {
            playerTwo = new Player(playerTwoCount % 4, playerTwoCount);
            
        }*/
        
    }

    public void playerThreeNext()
    {
        if (!takenColours.Contains(playerThreeCount))
        {
            if (playerModes[2].CompareTo("colour") == 0)
            {
                playerModes[2] = "weapon";
                playerThreeNextButtonText.text = "Done";
                playerThreeColour.gameObject.GetComponent<Image>().sprite = weapons[playerThreeCount];
                takenColours.Add(playerThreeCount);
                playerThreeButton.gameObject.SetActive(false);
            }
        }
        /*
        if(playerModes[2].CompareTo("weapon") == 0)
        {
            playerThree = new Player(playerThreeCount % 4, playerThreeCount);
            
        }*/
        
    }

    public void playerFourNext()
    {
        if (!takenColours.Contains(playerFourCount))
        {
            if (playerModes[3].CompareTo("colour") == 0)
            {
                playerModes[3] = "weapon";
                playerFourNextButtonText.text = "Done";
                playerFourColour.gameObject.GetComponent<Image>().sprite = weapons[playerFourCount];
                takenColours.Add(playerFourCount);
                playerFourButton.gameObject.SetActive(false);
            }
        }
        /*
        if(playerModes[3].CompareTo("weapon") == 0)
        {
            playerFour = new Player(playerFourCount % 4, playerFourCount);
           
        }*/
        
    }

    public void displayLosers()
    {
        Debug.Log(winner);
        GameObject[] panels = 
        {
            losersPanel.transform.Find("pOnePanel").gameObject,
            losersPanel.transform.Find("pTwoPanel").gameObject,
            losersPanel.transform.Find("pThreePanel").gameObject,
            losersPanel.transform.Find("pFourPanel").gameObject
        };

        for (int i = 0; i < panels.Length; i++)
        {
            if (i + 1 != winner)
            {
                panels[i].SetActive(true);
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
    }

    private List<int> randomUpgradeIndexes()
    {
        int count = 10;
        List<int> numbers = new List<int>();
        List<int> availableNumbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, availableNumbers.Count);
            numbers.Add(availableNumbers[index]);
            availableNumbers.RemoveAt(index);
        }

        return numbers;

    }


}
