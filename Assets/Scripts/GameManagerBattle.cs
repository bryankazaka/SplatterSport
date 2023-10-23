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
    private int[] playerUpgradeChoices= {0,0,0,0};

    public AudioClip btn_highlight;
    public AudioClip btn_click;

    public GameObject[] playerUpDesc;
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
    public List<(string,string)> upgradeText;

    public GameObject playerOneUpgradeSprite;
    public GameObject playerTwoUpgradeSprite;
    public GameObject playerThreeUpgradeSprite;
    public GameObject playerFourUpgradeSprite;

    private List<int> playerOneUpgrades;
    private List<int> playerTwoUpgrades;
    private List<int> playerThreeUpgrades;
    private List<int> playerFourUpgrades;

    public GameObject boardUp;
    public GameObject boardDrop;

    public TextMeshProUGUI tWinner;

    public GameObject endGame;
    
    private List<string> tutorialPrompts;

    public GameObject speechBubbleOne;
    public GameObject speechBubbleTwo;
    private bool[] upgradesActive = {false,false,false,false};
    public GameObject tutorialObj;
    public GameObject upgradeScreen;

    public AudioClip battleTheme;
    public AudioClip selectScreenTheme;
    public AudioClip crowdRoar;

    private AudioSource crowdAudioSource;
    public int numberOfPlayers = 0;

    void Start()
    {
        tRounds.text = "Points to win:" + "\n";
        tDrops.text = "Tutorial:" + "\n";
        audioSource = gameObject.AddComponent<AudioSource>();
        crowdAudioSource = gameObject.AddComponent<AudioSource>();
        crowdAudioSource.clip = crowdRoar;
        crowdAudioSource.volume = 0.2f;
        crowdAudioSource.loop = true;

        audioSource.volume = 0.3f;  
        audioSource.clip = selectScreenTheme;
        audioSource.loop = true;
        audioSource.Play();
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
        tutorialPrompts = new List<string> {"Try pressing ↑↓←→ or using the analogue on your controller rookie!", "Ever heard of left clicking or pressing RT/R2 to attack!?", "Come on! Hit mobs and splash more than your opponents!", "Watch the braziers on the top of the screen, thats your time limit!", "If you win a round i'll drop a banner for you!"};
        upgradeText = new List<(string,string)> {("Juggernaut","25% Stun Time"),
        ("Featherweight","+30% Attack Speed \n -10% Damage"),
        ("Bigger Splat","+20% Splat Size"),
        ("Flying Limbs","+20% Limb Speed \n 50% Bouncier"),
        ("Hard Hitter","+20% KnockBack \n +20% Limb Spread"),
        ("Disdainfull Stroke","+20% Damage"),
        ("Big Hands","+20% Weapon Size \n and Range"),
        ("Deft Brushstroke","+20% Attack Speed"),
        ("Artist Deadline","+20% Movement Speed"),
        ("Heavyweight","+30% Damage \n -10% Attack Speed")
        };
    }

    void Awake()
    {
        StartCoroutine(turnOneCharSelect());
    }

    IEnumerator tutorial()
    {
        yield return new WaitForSeconds(1.4f);
        if (!speechBubbleOne.activeInHierarchy)
        {
            speechBubbleOne.SetActive(true);
            speechBubbleOne.transform.Find("tCrowd").gameObject.GetComponent<TextMeshProUGUI>().text = tutorialPrompts[0];
        }

        yield return new WaitForSeconds(5.4f);
        if (speechBubbleOne.activeInHierarchy)
            speechBubbleOne.SetActive(false);
        
        if (!speechBubbleTwo.activeInHierarchy)
        {
            speechBubbleTwo.SetActive(true);
            speechBubbleTwo.transform.Find("tCrowd").gameObject.GetComponent<TextMeshProUGUI>().text = tutorialPrompts[1];
        }

        yield return new WaitForSeconds(5.4f);
        if (speechBubbleTwo.activeInHierarchy)
            speechBubbleTwo.SetActive(false);
        
        if (!speechBubbleOne.activeInHierarchy)
        {
            speechBubbleOne.SetActive(true);
            speechBubbleOne.transform.Find("tCrowd").gameObject.GetComponent<TextMeshProUGUI>().text = tutorialPrompts[2];
        }

        yield return new WaitForSeconds(5.4f);
        if (speechBubbleOne.activeInHierarchy)
            speechBubbleOne.SetActive(false);
        
        if (!speechBubbleTwo.activeInHierarchy)
        {
            speechBubbleTwo.SetActive(true);
            speechBubbleTwo.transform.Find("tCrowd").gameObject.GetComponent<TextMeshProUGUI>().text = tutorialPrompts[3];
        }

        yield return new WaitForSeconds(5.4f);
        if (speechBubbleTwo.activeInHierarchy)
            speechBubbleTwo.SetActive(false);
        
        if (!speechBubbleOne.activeInHierarchy)
        {
            speechBubbleOne.SetActive(true);
            speechBubbleOne.transform.Find("tCrowd").gameObject.GetComponent<TextMeshProUGUI>().text = tutorialPrompts[4];
        }

        yield return new WaitForSeconds(5.4f);
        if (speechBubbleOne.activeInHierarchy)
            speechBubbleOne.SetActive(false);
    }



    IEnumerator turnOneCharSelect()
    {
        yield return new WaitForSeconds(0.4f);
        charSelect.SetActive(true);
    }

    void Update()
    {
        
        tRounds.text = "Points to win:" + "\n" + roundCounter[roundIndex];
        tDrops.text = "Tutorial:" + "\n" + dropChoice[dropIndex];
            
        
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
        PlaySelectTheme();
        if (!upgradeScreen.activeSelf)
        {
            switch (playerNum)
            {
            case 0:
                if (left)
                {playerOneLeft();}
                else
                {playerOneRight();}
                break;
            case 1:
                if (left)
                {playerTwoLeft();}
                else
                {playerTwoRight();}
                break;
            case 2:
                if (left)
                {playerThreeLeft();}
                else
                {playerThreeRight();}
                break;
            case 3:
                if (left)
                {playerFourLeft();}
                else
                {playerFourRight();}
                break; 
            }
        }
        else
        {
           switch (playerNum)
            {
            case 0:
                if (left)
                {playerOneUpgradeLeft();}
                else
                {playerOneUpgradeRight();}
                break;
            case 1:
                if (left)
                {playerTwoUpgradeLeft();}
                else
                {playerTwoUpgradeRight();}
                break;
            case 2:
                if (left)
                {playerThreeUpgradeLeft();}
                else
                {playerThreeUpgradeRight();}
                break;
            case 3:
                if (left)
                {playerFourUpgradeLeft();}
                else
                {playerFourUpgradeRight();}
                break; 
            } 
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
        for (int i = 0; i < 4; i++)
        {
            upgradesActive[i] = false;
        }
        GetComponentInChildren<MainSpawner>().numberOfPlayers = numberOfPlayers;
        GetComponentInChildren<MainSpawner>().StartRound();
        GetComponentInChildren<TimerController>().enabled = true;
        GetComponentInChildren<PlayersManager>().StartGame();
        GetComponentInChildren<WallController>().setWinningScore(int.Parse(roundCounter[roundIndex]));
        GetComponentInChildren<TimerController>().StartAgain();
        
        if(dropIndex==0)
        {
            tutorialObj.gameObject.SetActive(true);
            StartCoroutine(tutorial());
        }
        upBoard();
    }

    public void playBattleTheme()
    {
        audioSource.Stop();
        audioSource.clip = battleTheme;
        audioSource.loop = true;
        audioSource.Play();
        crowdAudioSource.Play();
        
    }
    
    public void EndRound()
    {
        winner = GetComponentInChildren<SplatterController>().getWinner();
        Debug.Log("Player " + (winner +1)+ " won that round");
        GetComponentInChildren<WallController>().winPoint(winner);
        GetComponentInChildren<WallController>().UpdateWall(playerColors);
        GetComponentInChildren<CrowdController>().CrowdReset();
        GetComponentInChildren<MainSpawner>().EndRound();
        GetComponentInChildren<MobsManager>().EndRound();
        GetComponentInChildren<PlayersManager>().EndRound();
        inBattle = false;
        dropBoard();        
        displayLosers();
        PlaySelectTheme();
       
        //start the upgrade for losing players       
    }

    void PlaySelectTheme()
    {
        audioSource.Stop();
        crowdAudioSource.Stop();
        audioSource.clip = selectScreenTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void EndGame(int winningPlayer)
    {
        tWinner.text = "Winner: Player " + winningPlayer;
        endGame.SetActive(true);
        crowdAudioSource.Stop();
        audioSource.Stop();
        audioSource.clip = selectScreenTheme;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void addPlayer(GameObject player)
    {
        numberOfPlayers++;
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

    public void upgradePlayers()
    {
        
        int lenPlayers = 0;
        Transform playersT = GetComponentInChildren<PlayersManager>().transform;
        foreach (Transform player in playersT)
        {
            lenPlayers++;
        } 
        if (lenPlayers > 0)
        {
            players[0].GetComponent<PlayerController>().Upgrade(playerOneUpgrades[playerOneCount]);
        }
        if (lenPlayers > 1)
        {
            players[1].GetComponent<PlayerController>().Upgrade(playerTwoUpgrades[playerTwoCount]);
        }
        if (lenPlayers > 2)
        {
            players[2].GetComponent<PlayerController>().Upgrade(playerThreeUpgrades[playerThreeCount]);
        }
        if (lenPlayers > 3)
        {
            players[3].GetComponent<PlayerController>().Upgrade(playerFourUpgrades[playerFourCount]);
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
            
        }
    }

    public void PlayerNext(int playerNum)
    {
        if (!upgradeScreen.activeSelf)
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
            
    }

    public void playerOneNext()
    {
        
        if(playerModes[0].CompareTo("colour") == 0 && !takenColours.Contains(playerOneCount))
        {
                playerModes[0] = "weapon";
                playerOneNextButtonText.text = "Done";
                playerOneColour.gameObject.GetComponent<Image>().sprite = weapons[playerOneCount];
                playerOneColour.gameObject.GetComponent<Image>().color = colors[4];
                takenColours.Add(playerOneCount);
                players[0].GetComponent<PlayerController>().setPlayerColour(playerOneCount);
                
        }
        else
        if(playerModes[0].CompareTo("weapon") == 0)
        {
               
                int tempNum = (playerOneCount - playerOneCount%4)/4;
                players[0].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
                players[0].gameObject.GetComponent<PlayerController>().CreateWeapon(weapons[playerOneCount]);
                GameObject.Find("pOnePanel").SetActive(false);
                
                playerModes[0]= "ready";
            
        }
        
         
    }

    public void playerTwoNext()
    {
        
            if (playerModes[1].CompareTo("colour") == 0 && !takenColours.Contains(playerTwoCount))
            {
                playerModes[1] = "weapon";
                playerTwoNextButtonText.text = "Done";
                playerTwoColour.gameObject.GetComponent<Image>().sprite = weapons[playerTwoCount];
                playerTwoColour.gameObject.GetComponent<Image>().color = colors[4];
                takenColours.Add(playerTwoCount);
                players[1].GetComponent<PlayerController>().setPlayerColour(playerTwoCount);
              
            }
            else
            if(playerModes[1].CompareTo("weapon") == 0)
            {
                int tempNum = (playerTwoCount - playerTwoCount%4)/4;
                players[1].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
                players[1].gameObject.GetComponent<PlayerController>().CreateWeapon(weapons[playerTwoCount]);
                GameObject.Find("pTwoPanel").SetActive(false);
                playerModes[1]= "ready";            
            }
        
        
        
    }

    public void playerThreeNext()
    {
        
            if (playerModes[2].CompareTo("colour") == 0 && !takenColours.Contains(playerThreeCount))
            {
                playerModes[2] = "weapon";
                playerThreeNextButtonText.text = "Done";
                playerThreeColour.gameObject.GetComponent<Image>().sprite = weapons[playerThreeCount];
                playerThreeColour.gameObject.GetComponent<Image>().color = colors[4];
                takenColours.Add(playerThreeCount);
                players[2].GetComponent<PlayerController>().setPlayerColour(playerThreeCount);
                
            }
            else
            if(playerModes[2].CompareTo("weapon") == 0)
            {
                int tempNum = (playerThreeCount - playerThreeCount%4)/4;
                players[2].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
                players[2].gameObject.GetComponent<PlayerController>().CreateWeapon(weapons[playerThreeCount]);
                GameObject.Find("pThreePanel").SetActive(false);
                playerModes[2]= "ready";
                
            }
        
       
        
    }

    public void playerFourNext()
    {
        
            if (playerModes[3].CompareTo("colour") ==  0&& !takenColours.Contains(playerFourCount))
            {
                playerModes[3] = "weapon";
                playerFourNextButtonText.text = "Done";
                playerFourColour.gameObject.GetComponent<Image>().sprite = weapons[playerFourCount];
                playerFourColour.gameObject.GetComponent<Image>().color = colors[4];
                takenColours.Add(playerFourCount);
                players[3].GetComponent<PlayerController>().setPlayerColour(playerFourCount);
              
            }
            else
            if(playerModes[3].CompareTo("weapon") == 0)
            {
                int tempNum = (playerFourCount - playerFourCount%4)/4;
                players[3].GetComponent<PlayerController>().setPlayerWeapon(tempNum);
                players[3].gameObject.GetComponent<PlayerController>().CreateWeapon(weapons[playerFourCount]);
                GameObject.Find("pFourPanel").SetActive(false);
                playerModes[3]= "ready";
            }
        
        
        
    }


    public void displayLosers()
    {
       upgradeScreen.SetActive(true);
        GameObject[] panels = 
        {
            losersPanel.transform.Find("pOnePanel").gameObject,
            losersPanel.transform.Find("pTwoPanel").gameObject,
            losersPanel.transform.Find("pThreePanel").gameObject,
            losersPanel.transform.Find("pFourPanel").gameObject
        };
        int lenPlayers = 0;
        Transform players = GetComponentInChildren<PlayersManager>().transform;
        foreach (Transform player in players)
        {
            lenPlayers++;
        } 
        for (int i = 0; i < lenPlayers; i++)
        {
            
            if (i != winner)
            {
                panels[i].SetActive(true);
                upgradesActive[i] = true;
            }
            else
            {
                panels[i].SetActive(false);
            }
        }
        genUpgrades();
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

    public void genUpgrades()
    {
        playerOneCount = 0;
        playerOneUpgrades = randomUpgradeIndexes();
        playerOneUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerOneUpgrades[playerOneCount]];
        playerUpDesc[0].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item2;
        playerUpDesc[4].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item1;

        playerTwoCount = 0;
        playerTwoUpgrades = randomUpgradeIndexes();
        playerTwoUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerTwoUpgrades[playerTwoCount]];
        playerUpDesc[1].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item2;
        playerUpDesc[5].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item1;

        playerThreeCount = 0;
        playerThreeUpgrades = randomUpgradeIndexes();
        playerThreeUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerThreeUpgrades[ playerThreeCount]];
        playerUpDesc[2].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item2;
        playerUpDesc[6].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item1;

        playerFourCount = 0;
        playerFourUpgrades = randomUpgradeIndexes();
        playerFourUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerFourUpgrades[playerFourCount]];
        playerUpDesc[3].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item2;
        playerUpDesc[7].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item1;
    }

    public void playerOneUpgradeRight()
    {
        if (upgradesActive[0])         
            {playerOneCount = (playerOneCount + 1) % 3;
            playerOneUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerOneUpgrades[playerOneCount]];
            playerUpDesc[0].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item2;
            playerUpDesc[4].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item1;}
    }
    public void playerOneUpgradeLeft()
    {
        if (upgradesActive[0]) 
            {playerOneCount = (playerOneCount + 2) % 3;
            playerOneUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerOneUpgrades[playerOneCount]];
            playerUpDesc[0].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item2;
            playerUpDesc[4].GetComponent<TextMeshProUGUI>().text = upgradeText[playerOneUpgrades[playerOneCount]].Item1;}
    }

    public void playerTwoUpgradeRight()
    {
        if (upgradesActive[1]) 
            {playerTwoCount = (playerTwoCount + 1) % 3;
            playerTwoUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerTwoUpgrades[playerTwoCount]];
            playerUpDesc[1].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item2;
            playerUpDesc[5].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item1;}
        
    }
    public void playerTwoUpgradeLeft()
    {
        if (upgradesActive[1]) 
            {playerTwoCount = (playerTwoCount + 2) % 3;
            playerTwoUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerTwoUpgrades[playerTwoCount]];
            playerUpDesc[1].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item2;
            playerUpDesc[5].GetComponent<TextMeshProUGUI>().text = upgradeText[playerTwoUpgrades[playerTwoCount]].Item1;}
    }

    public void playerThreeUpgradeRight()
    {
        if (upgradesActive[2]) 
            {playerThreeCount = (playerThreeCount + 1) % 3;
            playerThreeUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerThreeUpgrades[playerThreeCount]];
            playerUpDesc[2].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item2;
            playerUpDesc[6].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item1;}
    }
    public void playerThreeUpgradeLeft()
    {
        if (upgradesActive[2]) 
            {playerThreeCount = (playerThreeCount + 2) % 3;
            playerThreeUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerThreeUpgrades[playerThreeCount]];
            playerUpDesc[2].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item2;
            playerUpDesc[6].GetComponent<TextMeshProUGUI>().text = upgradeText[playerThreeUpgrades[playerThreeCount]].Item1;}
    }

    public void playerFourUpgradeRight()
    {
        if (upgradesActive[3]) 
           { playerFourCount = (playerFourCount + 1) % 3;
            playerFourUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerFourUpgrades[playerFourCount]];
            playerUpDesc[3].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item2;
            playerUpDesc[7].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item1;}
    }
    public void playerFourUpgradeLeft()
    {
        if (upgradesActive[3]) 
            {playerFourCount = (playerFourCount + 2) % 3;
            playerFourUpgradeSprite.gameObject.GetComponent<Image>().sprite = upgrades[playerFourUpgrades[playerFourCount]];
            playerUpDesc[3].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item2;
            playerUpDesc[7].GetComponent<TextMeshProUGUI>().text = upgradeText[playerFourUpgrades[playerFourCount]].Item1;}
    }


    public void dropBoard()
    {
        boardDrop.gameObject.SetActive(true);
        boardUp.gameObject.SetActive(false);
    }

    public void upBoard()
    {
        boardUp.gameObject.SetActive(true);
        boardDrop.gameObject.SetActive(false);
    }

    public void backToMain()
    {
        StartCoroutine(backToMainDelay());
    }

    IEnumerator backToMainDelay()
    {
        yield return new WaitForSeconds(0.417f);
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousSceneIndex);
    }

}
