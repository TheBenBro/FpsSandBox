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
    public bool canSpawnItems;
    bool isPlayerSpawned = false;
    string level;
    GameState gameState;
    public float timerLimit;
    public int roomsSpawned = 0;
    public enum GameState { StartGame, GameOver, Menu, Paused};

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
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
        //Debug.Log(playerSettings.mouseSensitivity);
    }
    public void SetGameState(GameState gameState_)
    {
        gameState = gameState_;
    }
    public bool GetGameState(GameState gameState_)
    {
        return gameState == gameState_;
    }
    public void resetTimer()
    {
        currentTime = 0f;
    }
    public float GetTimer()
    {
        return currentTime;
    }
    public void ResetPlayer()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = GameObject.Instantiate(respawn.player, respawn.spawnLocations.transform.position, Quaternion.identity);
    }
    public IEnumerator SpawnPlayer()
    {
        
        //Scene[] scenes = SceneManager.GetAllScenes();
        //Debug.Log(scenes[1].name);
        //foreach (Scene sc in scenes)
        //    if(sc.name == level)
        //    {
        //        yield return new WaitUntil(() => sc.name == level);
        //    }
        
        respawn.FindNewSpawnLocations();
        //Debug.Log("Spawned level");
        if (player != null)
        {
            Destroy(player);
        }
        player = GameObject.Instantiate(respawn.player, respawn.spawnLocations.transform.position, Quaternion.identity);
        currentTime = 0.0f;
        SetGameState(GameState.StartGame);
        //Debug.Log("Spawned Player");
        yield return null;
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
        isPlayerSpawned = false;
    }
    public void SetLevel(string level_)
    {
        level = level_;
    }

    private void NotifyChangedScene(Scene current, Scene next)
    {
        Debug.Log("Scene Changed!");
        canSpawnItems = false;
    }
    public void ShowFPS(Toggle state_)
    {
        playerSettings.SetShowFPS(state_.isOn);
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
        if(roomsSpawned >= playerSettings.GetMaxRooms())
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("RoomCollider");

            foreach(GameObject obj in objs)
            {
                obj.GetComponent<Collider>().isTrigger = true;
                
            }
            if (!isPlayerSpawned)
            {
                StartCoroutine(SpawnPlayer());
                isPlayerSpawned = true;
            }
            canSpawnItems = true;
        }
        return roomsSpawned >= playerSettings.GetMaxRooms();
    }
    public void SetCanSpawnItems(bool state_)
    {
        canSpawnItems = state_;
    }
    public void ResetRoomsSpawned()
    {
        roomsSpawned = 0;
    }
    public void SetMaxRooms(int rooms_)
    {
        playerSettings.SetMaxRooms(rooms_);
    }
}
