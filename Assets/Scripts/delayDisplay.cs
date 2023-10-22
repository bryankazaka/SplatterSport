using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delayDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        StartCoroutine(Display());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Display()
    {
        yield return new WaitForSeconds(0.517f);
        gameObject.SetActive(true);
    }
}
