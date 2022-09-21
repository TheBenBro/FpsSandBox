using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FlyingCameraHud : MonoBehaviour
{
    private int frameCount;
    public TMP_Text fps;
    private float pollingTime = 1f;
    private float time;
    public GenerateMap map;
    public GameObject menu;
    public GameObject options;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OpenMenu()
    {
        if (menu.activeSelf == false && options.activeSelf == false)
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            menu.SetActive(false);
            options.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            map.StartSpawning();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {

            OpenMenu();
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
        if (GameManager.Instance.playerSettings.showFPS == false)
        {
            fps.enabled = false;
        }
    }
}
