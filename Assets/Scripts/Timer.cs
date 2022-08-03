using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    public float currentTime;
    public bool countDown;
    public bool hasLimit;
    public float timerLimit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
        if (hasLimit && (countDown && currentTime <= timerLimit) ||(!countDown && currentTime >= timerLimit))
        {
            currentTime = timerLimit;
            enabled = false;
        }
       
    }
    private void SetTimerText()
    {
        timerText.text = currentTime.ToString();
    }
    public float GetTimer()
    {
        return currentTime;
    }
}
