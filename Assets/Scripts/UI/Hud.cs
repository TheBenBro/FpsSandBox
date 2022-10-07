using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hud : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthBar;
    public TMP_Text counter;
    public TMP_Text timer;
    public TMP_Text fps;
    Target target;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = target.GetHealth();
        counter.SetText(GameManager.Instance.GetCounter().ToString());
        timer.SetText(GameManager.Instance.GetTimer().ToString("0.00")); 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause");
        }
        if (GameManager.Instance.playerSettings.showFPS == true)
        {
            fps.enabled = true;

            // Update time.
            time += Time.deltaTime;

            // Count this frame.
            frameCount++;

            if (time >= pollingTime)
            {
                // Update frame rate.
                int frameRate = Mathf.RoundToInt((float)frameCount / time);
                fps.text = frameRate.ToString() + " fps";

                // Reset time and frame count.
                time -= pollingTime;
                frameCount = 0;
            }
        }
        else
        {
            fps.enabled = false;
        }

    }
}
