using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceCloseButton : MonoBehaviour
{
    public UnityEngine.UI.Button closeButton; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnClick()
    {
        closeButton.onClick.Invoke();
    } 

}
