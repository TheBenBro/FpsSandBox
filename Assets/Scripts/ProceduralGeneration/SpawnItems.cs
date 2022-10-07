using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private void Awake()
    {
        EventManager.roomEvent += SpawnItem;
        Debug.Log("Ready!");
    }
    private void OnDestroy()
    {
        EventManager.roomEvent -= SpawnItem;
    }
    void SpawnItem()
    {
        Instantiate(item, this.transform.position, Quaternion.identity);
        Debug.Log(item.name);
    }
}
