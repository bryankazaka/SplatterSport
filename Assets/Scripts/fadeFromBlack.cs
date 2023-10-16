using UnityEngine;
using UnityEngine.UI; 
using System.Collections;


public class ButtonEffect : MonoBehaviour
{
    private Image imageComponent; 
    public float duration = 1.5f;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(FadeOutImage(1f));
    }

    IEnumerator FadeOutImage(float duration)
    {
        Color originalColor = imageComponent.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); 

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            imageComponent.color = Color.Lerp(originalColor, targetColor, elapsed / duration);
            yield return null; 
        }

        imageComponent.color = targetColor; 
    }
}
