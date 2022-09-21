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
    public PlayerSettings playerSettings;
    public HandleWindow window;
    public float currentTime;
    public bool countDown;
    public bool hasLimit;
    string level;
    GameState gameState;
    public float timerLimit;
    public int roomsSpawned = 0;
    public enum GameState { StartGame, GameOver, Menu, Paused};
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreenMode = Screen.fullScreenMode;
        SceneManager.activeSceneChanged += NotifyChangedScene;
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
        //Debug.Log(scenes[1].name);
        foreach (Scene sc in scenes)
            if(sc.name == level)
            {
                yield return new WaitUntil(() => sc.name == level);
            }
        respawn.FindNewSpawnLocations();
        //Debug.Log("Spawned level");
        GameObject.Instantiate(respawn.player, respawn.spawnLocations.transform.position, Quaternion.identity);
        currentTime = 0.0f;
        SetGameState(GameState.StartGame);
        //Debug.Log("Spawned Player");
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        StartCoroutine(SpawnPlayer());
    }
    public void SetLevel(string level_)
    {
        level = level_;
    }

    private void NotifyChangedScene(Scene current, Scene next)
    {
        Debug.Log("Scene Changed!");
       
    }
    public void ShowFPS(Toggle state_)
    {
        playerSettings.showFPS = state_.isOn;
    }

    public void AddRoom()
    {
        roomsSpawned++;
       // Debug.Log(roomsSpawned.ToString());
    }
    public void RemoveRoom()
    {
        roomsSpawned--;
        //Debug.Log(roomsSpawned.ToString());
    }
    public bool SpawnLimitReached()
    {
        return roomsSpawned >= playerSettings.maxRooms;
    }
    public void ResetRoomsSpawned()
    {
        roomsSpawned = 0;
    }
    public void SetMaxRooms(int rooms_)
    {
        playerSettings.maxRooms = rooms_;
    }
}
