using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Text sensValue;
    public TMP_Text fpsValue;
    public TMP_Text maxRoomsValue;
    public TMP_Text screenResolution;
    public Slider audioSlider;
    public Slider sensSlider;
    public Slider fpsSlider;
    public Slider roomsSlider;
    public Toggle fpsToggle;
    public TMP_Dropdown dropDown;
    private void Start()
    {
        sensSlider.value = GameManager.Instance.playerSettings.mouseSensitivity;
        roomsSlider.value = GameManager.Instance.playerSettings.maxRooms;
        fpsSlider.value = GameManager.Instance.playerSettings.targetFrameRate;
        GameManager.Instance.playerSettings.showFPS = fpsToggle.isOn;
        sensValue.SetText(GameManager.Instance.playerSettings.mouseSensitivity.ToString());
        fpsValue.SetText(GameManager.Instance.playerSettings.targetFrameRate.ToString());
        maxRoomsValue.SetText(GameManager.Instance.playerSettings.maxRooms.ToString());
    }
    public void SetVolume()
    {
        audioMixer.SetFloat("Volume", audioSlider.value);
    }

    public void SetSens()
    {
        GameManager.Instance.playerSettings.mouseSensitivity = sensSlider.value;
        sensValue.SetText(sensSlider.value.ToString());
    }
    public void SetFrameLimit()
    {
        GameManager.Instance.playerSettings.targetFrameRate = (int)fpsSlider.value;
        Application.targetFrameRate = (int)fpsSlider.value;
        fpsValue.SetText(fpsSlider.value.ToString());
    }
    public void SetMaxRooms()
    {
        GameManager.Instance.playerSettings.maxRooms = (int)roomsSlider.value;
        GameManager.Instance.SetMaxRooms((int)roomsSlider.value);
        maxRoomsValue.SetText(roomsSlider.value.ToString());
    }
    public void ShowFPS(Toggle state_)
    {
        GameManager.Instance.playerSettings.showFPS = state_.isOn;
    }

    public void ChangeWindowMode()
    {
        GameManager.Instance.window.HandleWindowMode(dropDown.value);
    }

    public void IncreaseScreenResolution()
    {
        GameManager.Instance.window.IncreaseResolution(screenResolution);
    }
    public void DecreaseScreenResolution()
    {
        GameManager.Instance.window.DecreaseResolution(screenResolution);
        //screenResolution.SetText(Screen.currentResolution.ToString());
    }
}

