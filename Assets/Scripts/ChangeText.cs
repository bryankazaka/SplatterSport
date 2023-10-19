using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  
using TMPro;  
public class ChangeText : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI thisText; 
    private string playNum;
    private string mode;
    
    void Start()
    {
        thisText = gameObject.GetComponent<TextMeshProUGUI>();
        playNum = thisText.text;
        mode = "colour";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chooseWeapon()
    {
        thisText.text = "Choose Weapon:";
    }

    public void chooseColour()
    {
        thisText.text = "Choose Colour:";
    }

    public void playerNum()
    {
        thisText.text = playNum;
    }

    /**
    private void onClick()
    {
        if(mode.CompareTo("colour")==1)
        {
            mode = "weapon";
        }
    }*/

}
