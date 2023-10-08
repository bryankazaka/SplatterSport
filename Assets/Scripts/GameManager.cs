using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip btn_highlight;
    public AudioClip btn_click;
    public AudioClip bgmMenu;
    private AudioSource audioSource;
    void Start()
    {
         audioSource = gameObject.GetComponent<AudioSource>();
         audioSource.volume = 0.3f;
         audioSource.clip = bgmMenu;
         audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
