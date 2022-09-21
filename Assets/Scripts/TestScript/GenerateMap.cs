using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : Singleton<GenerateMap>
{
    public GameObject[] roomsPrefab;
    public GameObject wallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        GameManager.Instance.ResetRoomsSpawned();
        foreach (GameObject tmp in rooms)
        {
            Destroy(tmp);
        }
        Instantiate(roomsPrefab[0], this.transform);
    }

    public void StartSpawning()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        GameManager.Instance.ResetRoomsSpawned();
        foreach (GameObject tmp in rooms)
        {
            Destroy(tmp);
        }
        StartCoroutine(RoomGenerationDelay());
    }

    IEnumerator RoomGenerationDelay()
    {
        yield return new WaitForSeconds(1);
        Instantiate(roomsPrefab[0], this.transform);
    }
}
