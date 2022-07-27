using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Respawn : MonoBehaviour
{
    public GameObject[] spawnLocations;
    public GameObject player;

    private Vector3 respawnLocation;
    int spawn;
    private void OnEnable()
    {
        GameManager.RespawnPlayer += DestroyPlayer;
    }
    private void OnDisable()
    {
        GameManager.RespawnPlayer -= DestroyPlayer;
    }
    private void Awake()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    private void Start()
    {
        player = (GameObject)Resources.Load("Character", typeof(GameObject));
        respawnLocation = player.transform.position;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        spawn = Random.Range(0, spawnLocations.Length);
        GameObject.Instantiate(player, spawnLocations[spawn].transform.position, Quaternion.identity);
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
