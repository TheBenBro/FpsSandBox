using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Respawn : MonoBehaviour
{
    public GameObject spawnLocations;
    public GameObject player;
    int spawn;
    private void OnEnable()
    {
        GameManager.RespawnPlayer += DestroyPlayer;
    }
    private void OnDisable()
    {
        GameManager.RespawnPlayer -= DestroyPlayer;
    }
    public void FindNewSpawnLocations()
    {
        player = (GameObject)Resources.Load("Character", typeof(GameObject));
        spawnLocations = GameObject.Find("Respawn Point");
    }

    private void SpawnPlayer()
    {
       // spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(player, spawnLocations.transform.position, Quaternion.identity);
    }
    private void DestroyPlayer(Target target_)
    {
        if (target_ != null)
        {
            DestroyImmediate(target_.gameObject);
            SpawnPlayer();
        }
    }
}
