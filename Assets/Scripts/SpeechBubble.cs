using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    private float animationDuration;

    private Vector3 initialScale; 
    private Coroutine animationRoutine; 

    private void Awake()
    {
        initialScale = transform.localScale; 
        animationDuration = 0.3f; 
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        animationRoutine = StartCoroutine(ScaleOverTime(Vector3.zero, initialScale, animationDuration));
    }

    private void OnDisable()
    {
        StopCoroutine(animationRoutine); 
        transform.localScale = Vector3.zero; 
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = endScale;
    }
}
