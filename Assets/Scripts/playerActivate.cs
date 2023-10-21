using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerInputManager>().enabled = true;
    }

    // Update is called once per frame
    
}
