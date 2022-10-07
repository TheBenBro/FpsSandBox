using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action roomEvent;
    bool itemsSpawned = false;
    private void Update()
    {
        if (GameManager.Instance.canSpawnItems && !itemsSpawned)
        {
            Debug.Log("Spawn Items!!!");
            roomEvent?.Invoke();
            itemsSpawned = true;
        }
        else if (!GameManager.Instance.canSpawnItems)
        {
            itemsSpawned = false;
        }
    }
}
