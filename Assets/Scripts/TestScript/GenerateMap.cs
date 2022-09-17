using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : Singleton<GenerateMap>
{
    public GameObject[] roomsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach (GameObject tmp in rooms)
        {
            Destroy(tmp);
        }
        Instantiate(roomsPrefab[0], this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
