using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip btn_highlight;
    public AudioClip btn_click;
    public AudioClip startSplat;
    public AudioClip bgmMenu;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI rules;
    public GameObject boardDrop;

    public Button btnClose;

    public Slider slider;
    public AudioSource audioSource;

    public GameObject zoom;

    public GameObject rulesMenu;
    public GameObject sVolume;

    //public float transitionTime = 2f;

    private string[] rulesText = new string[]
    {
        "About:\nDive into the 'Colour Colliseum' and battle friends in a vibrant PVE arena. Spread your paint by defeating mobs, and claim victory by covering the most ground. Beware! Losers get to choose power-ups, so strategize wisely!",
        "Goal:\nDominate by covering the arena with your paint color. Defeat continuously spawning enemies, causing them to explode into paint splats. Overpaint your opponent's marks for the win! In this realm, mastery over color crowns you the Grand Master of Colors.",
        "Tips:\nLosing a round might offer strategic upgrades. The dynamic crowd showcases the leading player by donning their color.",
        "Controls:\nMove: Directional Keys or Controller Analogue\nAttack: Left Click or RT/R2"
    };

    private string[] roundCounter = new string[] { "3", "5", "7" };
    private string[] dropChoice = new string[] { "Enabled", "Disabled" };
    [SerializeField] private int currentRule;
    private int lenRules = 4;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(startSplat, 1.0f);
    }

    void Start()
    {
        audioSource.volume = 0.3f;
        audioSource.clip = bgmMenu;
        audioSource.Play();
        currentRule = 0;
        rules.text = rulesText[currentRule];
    }

    void Update()
    {
        volumeText.text = Mathf.FloorToInt(slider.value * 10.0f).ToString();
        rules.text = rulesText[currentRule] + "\n\n" + (currentRule + 1).ToString() + "/" + lenRules.ToString();
    }

    public void buttonExit()
    {
        Application.Quit();
    }

    public void btnHighlight()
    {
        audioSource.PlayOneShot(btn_highlight, 6.0f);
    }

    public void btnClick()
    {
        audioSource.PlayOneShot(btn_click, 6.0f);
    }

    public void rulesTextBack()
    {
        if (currentRule != 0)
        {
            currentRule -= 1;
        }
    }

    public void rulesTextForward()
    {
        if (currentRule != lenRules - 1)
        {
            currentRule += 1;
        }
    }

    public void roundsTextForward()
    {
    }

    public void roundsTextBackwards()
    {
    }

    public void dropsTextForward()
    {
    }

    public void dropsTextBackwards()
    {
    }


    public void fadeBlack()
    {
        StartCoroutine(LoadBattleScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadBattleScene(int sceneIndex)
    {
        yield return new WaitForSeconds(0.85f);
        SceneManager.LoadScene(sceneIndex);
    }

    public void clearMenu()
    {
        if (boardDrop.gameObject.activeSelf)
        {
            btnClose.onClick.Invoke();
        }
    }

    IEnumerator delaySettingsClick()
    {
        yield return new WaitForSeconds(0.417f);
        sVolume.gameObject.SetActive(true);
        btnClose.gameObject.SetActive(true);
    }

    IEnumerator delayRulesClick()
    {
        yield return new WaitForSeconds(0.417f);
        rulesMenu.gameObject.SetActive(true);
        btnClose.gameObject.SetActive(true);
    }

    public void settingsClick()
    {
        StartCoroutine(delaySettingsClick());
    }

    public void rulesClick()
    {
        StartCoroutine(delayRulesClick());
    }
}