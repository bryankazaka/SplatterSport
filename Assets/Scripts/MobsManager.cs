using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void EndRound()
    {
        foreach (Transform mob in transform)
        {
            GameObject.Destroy(mob.gameObject);
        }
    }
}
