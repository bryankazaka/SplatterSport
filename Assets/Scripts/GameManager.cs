using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;  
public class GameManager : MonoBehaviour
{
    public AudioClip btn_highlight;
    public AudioClip btn_click;
    public AudioClip bgmMenu;
    public TextMeshProUGUI volumeText;  
    public TextMeshProUGUI rules;  
    public Slider slider;
    private AudioSource audioSource;
    private string[] rulesText = new string[] {
        "Dive into the 'Colour Colliseum' and battle friends in a vibrant PVE arena. Spread your paint by defeating mobs, and claim victory by covering the most ground. Beware! Losers get to choose power-ups, so strategize wisely!",
        "Goal:\nDominate by covering the arena with your paint color. Defeat continuously spawning enemies, causing them to explode into paint splats. Overpaint your opponent's marks for the win! In this realm, mastery over color crowns you the Grand Master of Colors.",
        "Tips:\nLosing a round might offer strategic upgrades. The dynamic crowd showcases the leading player by donning their color.",
        "Engage, strategize, and paint your way to victory!",
        "Controls:\nMove: Directional Keys\nAttack/Pickup/Use: R2\nDodge: L2"
    };
    [SerializeField] private int currentRule;
    void Start()
    {
         audioSource = gameObject.GetComponent<AudioSource>();
         audioSource.volume = 0.3f;  
         audioSource.clip = bgmMenu;
         audioSource.Play();
         currentRule = 0;
         rules.text = rulesText[currentRule];
    }

    void Update()
    {
        volumeText.text = Mathf.FloorToInt(slider.value*10.0f).ToString(); 
        rules.text = rulesText[currentRule];
    }

    public void buttonExit()
    {
        Debug.Log("App closed");
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
        if (currentRule!= 0)
        {
            currentRule-=1;
        }
        
    }
    public void rulesTextForward()
    {
        currentRule+=1;
    }
}
