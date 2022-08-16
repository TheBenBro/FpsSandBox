using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    int counter = 0;
    public static Action<Target> RespawnPlayer;
    public Respawn respawn;
    public float currentTime;
    public bool countDown;
    public bool hasLimit;
    GameState gameState;
    public float timerLimit;
    public enum GameState { StartGame, GameOver, Menu};
    // Start is called before the first frame update
    void Start()
    {
        counter = GameObject.FindGameObjectsWithTag("Target").Length;
        gameState = GameState.Menu;
        //timer = GetComponent<Timer>();
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
            SetGameState(GameState.GameOver);
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
    void Update()
    {
        //currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        if (gameState == GameState.StartGame)
        {
            currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        }
    }
    public void SetGameState(GameState gameState_)
    {
        gameState = gameState_;
    }
    public void resetTimer()
    {
        currentTime = 0f;
    }
    public float GetTimer()
    {
        return currentTime;
    }
    public IEnumerator SpawnPlayer()
    {
        
        Scene[] scenes = SceneManager.GetAllScenes();
        Debug.Log(scenes[1].name);
        foreach (Scene sc in scenes)
            if(sc.name == "FirstMap")
            {
                yield return new WaitUntil(() => sc.name == "FirstMap");
            }
           
        Debug.Log("Spawned level");
        GameObject.Instantiate(respawn.player, respawn.spawnLocations.transform.position, Quaternion.identity);
        SetGameState(GameState.StartGame);
        Debug.Log("Spawned Player");
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("FirstMap", LoadSceneMode.Additive);
        StartCoroutine(SpawnPlayer());
    }
}
