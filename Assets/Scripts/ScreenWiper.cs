using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenWiper : MonoBehaviour
{
    public Animator transitionOne;
    public Animator transitionTwo;
    public float transitionTime = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScreenWipe()
    {
       StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex+1));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        
        //transitionOne.SetTrigger("Switch");
        //transitionTwo.SetTrigger("Switch");

        yield return new WaitForSeconds(transitionTime);

        
        SceneManager.LoadScene(sceneIndex);
        
    }
}
