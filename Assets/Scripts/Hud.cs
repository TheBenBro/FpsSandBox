using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthBar;
    public Text counter;
    Target target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = target.GetHealth();
        counter.text = GameManager.Instance.GetCounter().ToString();

    }
}
