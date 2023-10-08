using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, ISelectHandler , IPointerEnterHandler
{
    // Start is called before the first frame update
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
