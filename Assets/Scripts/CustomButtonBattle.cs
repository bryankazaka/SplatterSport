using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro; 

public class CustomButtonBattle : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    public GameManagerBattle gameManagerBattle;

    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        
    }
    public void Fade()
    {
        StartCoroutine(FadeOutText(0.4f));
    }
    
    IEnumerator FadeOutText(float duration)
    {
        Color originalColor = tmpText.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            tmpText.color = Color.Lerp(originalColor, targetColor, elapsed / duration);
            yield return null; 
        }

        tmpText.color = targetColor; 
    }

    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameManagerBattle.btnHighlight();
    }

    public void OnSelect(BaseEventData eventData)
    {
        
        gameManagerBattle.btnClick();
    }
}
