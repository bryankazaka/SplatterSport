using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public GameManager gameManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameManager.btnHighlight();
    }

    public void OnSelect(BaseEventData eventData)
    {
        
        gameManager.btnClick();
    }
}
