using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour
{
    // Start is called before the first frame update
    private List<GameObject> spawnPoints = new List<GameObject>();
    public bool canSpawnRooms = false;
    bool spawnAttemptsMaxed = false;
    public GameObject room;
    int spawnAttempts = 0;
    int roomInt  = 0;
    int spawnPointInt;
    public Collider roomCollider;
    public LayerMask mask;
    public int spawnPointI;
    private void Awake()
    {
        //Debug.Log("Ready To Spawn!");
        SpawnRoom();
        
    }
    public void SpawnRoom()
    {
       
        if (!GameManager.Instance.SpawnLimitReached())
        {
            roomInt = Random.Range(1, 5);
        }
        else
        {
            //Debug.Log("Closing Off Room");
            roomInt = 6;
        }

        room = Instantiate(GenerateMap.Instance.roomsPrefab[roomInt]);
        Debug.Log("Spawning " + room.name);
        GameManager.Instance.AddRoom();
        spawnAttempts++;
        foreach (Transform child in room.transform)
        {
            if (child.tag == "RoomSpawns")
            {
                Debug.Log("Adding " + child.gameObject.name);
                spawnPoints.Add(child.gameObject);
            }
        }
        spawnPointInt = Random.Range(0, spawnPoints.Count);
        room.transform.position = new Vector3(transform.position.x - spawnPoints[spawnPointInt].transform.position.x, transform.position.y - spawnPoints[spawnPointInt].transform.position.y, transform.position.z - spawnPoints[spawnPointInt].transform.position.z);
        while (spawnPoints[spawnPointInt].transform.forward != -transform.forward)
        {
            room.transform.RotateAround(spawnPoints[spawnPointInt].transform.position, Vector3.up, 90);
        }

        //spawnPoints[spawnPointInt].GetComponent<SpawnRooms>().SetCanSpawnRooms(false);


        if (CheckCollisions())
        {

            if (spawnAttempts <= 5)
            {
                Debug.Log("TryingAgain");
                GameManager.Instance.RemoveRoom();
                spawnPoints.Clear();
                Destroy(room);
                SpawnRoom();
            }
            else
            {
                Debug.Log("Deleting Room");
                GameManager.Instance.RemoveRoom();
                spawnPoints.Clear();
                Destroy(room);
            }
        }
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (i != spawnPointInt)
            {
                Debug.Log("Enabled Spawn");
                spawnPoints[i].SetActive(true);
            }
        }

    }
    //Checks csollision for each room when spawns
    private bool CheckCollisions()
    {
        if (room != null)
        {
            Physics.SyncTransforms();
            Collider[] box = Physics.OverlapBox(spawnPoints[spawnPointInt].GetComponent<SpawnRooms>().roomCollider.transform.position, spawnPoints[spawnPointInt].GetComponent<SpawnRooms>().roomCollider.bounds.size / 2, Quaternion.identity, mask);
            if (box.Length > 0)
            {
               
                foreach (Collider c in box)
                {
                    {
                        if (c != roomCollider && c != spawnPoints[spawnPointInt].GetComponent<SpawnRooms>().roomCollider)
                        {
                            Debug.Log(spawnPoints[spawnPointInt].GetComponent<SpawnRooms>().roomCollider.gameObject.transform.parent.name);
                            Debug.Log(c.gameObject.transform.parent.name);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }
    public void SetCanSpawnRooms(bool canSpawn_)
    {
        canSpawnRooms = canSpawn_;
    }

}
