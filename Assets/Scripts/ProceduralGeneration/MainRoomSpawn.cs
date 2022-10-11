using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRoomSpawn : MonoBehaviour
{
    private List<GameObject> spawnPoints = new List<GameObject>();
    public GameObject room;
    int roomInt;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRoom();
    }

    void SpawnRoom()
    {
        //
        roomInt = Random.Range(1, GenerateMap.Instance.roomsPrefab.Length - 1);
        room = Instantiate(GenerateMap.Instance.roomsPrefab[roomInt]);
        GameManager.Instance.AddRoom();
        foreach (Transform child in room.transform)
        {
            if (child.tag == "RoomSpawns")
            {
                spawnPoints.Add(child.gameObject);
            }
        }
        int spawnPointInt = Random.Range(0, spawnPoints.Count);
        room.transform.position = new Vector3(transform.position.x - spawnPoints[spawnPointInt].transform.position.x, transform.position.y - spawnPoints[spawnPointInt].transform.position.y, transform.position.z - spawnPoints[spawnPointInt].transform.position.z);
        Vector3 dir = (spawnPoints[spawnPointInt].transform.position - transform.parent.position).normalized;
        while (spawnPoints[spawnPointInt].transform.forward != -transform.forward)
        {
            room.transform.RotateAround(spawnPoints[spawnPointInt].transform.position, Vector3.up, 90);
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i != spawnPointInt)
            {
                //spawnPoints[i].GetComponent<SpawnRooms>().SetCanSpawnRooms(true);
                spawnPoints[i].gameObject.SetActive(true);
            }
        }
    }
}
