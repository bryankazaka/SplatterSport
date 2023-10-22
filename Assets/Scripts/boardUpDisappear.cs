using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardUpDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        makeDisappear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void makeDisappear()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.517f);
        gameObject.SetActive(false);
    }
}
