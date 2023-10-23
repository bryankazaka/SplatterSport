using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimerController : MonoBehaviour
{
    public int timerSeconds;
    public TileBase[] brazierStates;
    private Vector3Int pos;
    public Tilemap timerMap;
    private float startUp;
    private bool litUp;
    public AudioClip flameSizzle;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float secondsPerUpdate = ((float)timerSeconds)/(26.0f); //calculate the amount of time between brazier tile updates
        litUp=false;
        startUp = Time.time;
        
        pos = new Vector3Int(12,8,0);
         //repeat the animation
    }

    public void StartAgain()
    {
        startUp = Time.time;
        litUp = false;
        pos = new Vector3Int(12,8,0);
    }
    private void Update() 
    {
        if (Time.time - startUp > 0.7f)
        {
            if (!litUp)
            {
                
                litUp= true;
                Vector3Int TempPos = new(12,8,0); // the leftmost position of the braziers
                for (int i = 0; i < 26; i++)
                {
                    
                   
                    
                   
                    timerMap.SetTile(TempPos,brazierStates[0]);

                    timerMap.SetAnimationFrame(TempPos,Random.Range(0,9));
                    TempPos.x -= 1;
                    
                }
                float secondsPerUpdate = ((float)timerSeconds)/(26.0f);
                InvokeRepeating(nameof(Light), secondsPerUpdate, secondsPerUpdate);
            }
                 
        }
       
    }
    void Light()
    {
        audioSource.PlayOneShot(flameSizzle, 0.1f);
        
        timerMap.SetTile(pos,brazierStates[1]); //set the tile for the brazier
        timerMap.SetAnimationFrame(pos,0);
        pos.x -= 1;
        
        if (pos.x < -13) // when the timer runs out destroy the object {add end round function here}
        {
            
            CancelInvoke(nameof(Light));
            StartCoroutine( EndRound());
        }
    }

    IEnumerator EndRound()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponentInParent<GameManagerBattle>().EndRound();
    }

}
