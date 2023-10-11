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
    public Slider slider;
    private AudioSource audioSource;

    void Start()
    {
         audioSource = gameObject.GetComponent<AudioSource>();
         audioSource.volume = 30.0f;  
         audioSource.clip = bgmMenu;
         audioSource.Play();
    }

    void Update()
    {
        volumeText.text = Mathf.FloorToInt(slider.value).ToString(); 
    }

    public void buttonExit()
    {
        Debug.Log("App closed");
        Application.Quit();
    }

    public void btnHighlight()
    {
        audioSource.PlayOneShot(btn_highlight, 200.0f);
    }

    public void btnClick()
    {
        audioSource.PlayOneShot(btn_click, 200.0f);
    }
}
