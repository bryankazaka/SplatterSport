using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeSelect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameManagerBattle gameManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() 
    {
        gameManager.gameObject.GetComponent<GameManagerBattle>().displayLosers();
        gameManager.gameObject.GetComponent<GameManagerBattle>().genUpgrades();
        gameManager.gameObject.GetComponent<GameManagerBattle>().dropBoard();
    }

    void OnDisable() 
    {
        gameManager.gameObject.GetComponent<GameManagerBattle>().upBoard();
    }
  
}
