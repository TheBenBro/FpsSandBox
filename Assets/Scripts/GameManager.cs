using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    int counter = 0;
    public static Action<Target> RespawnPlayer;
    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.FindGameObjectsWithTag("Target").Length;
    }
    public int GetCounter()
    {
        return counter;
    }
    public void UpdateTargetCounter()
    {
        counter = GameObject.FindGameObjectsWithTag("Target").Length -1;
        if(counter == 0)
        {
            Debug.Log("GameOver");
        }
    }
}
