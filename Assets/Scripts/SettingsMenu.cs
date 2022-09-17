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
    public Slider sensSlider;
    public Slider fpsSlider;
    public Toggle fpsToggle;
    private void Start()
    {
        sensSlider.value = GameManager.Instance.playerSettings.mouseSensitivity;
        fpsSlider.value = 400;
        GameManager.Instance.playerSettings.showFPS = fpsToggle.isOn;
        sensValue.SetText(GameManager.Instance.playerSettings.mouseSensitivity.ToString());
        
    }
    public void SetVolume(float volume_)
    {
        audioMixer.SetFloat("Volume", volume_);
    }

    public void SetSens()
    {
        GameManager.Instance.playerSettings.mouseSensitivity = sensSlider.value;
        sensValue.SetText(sensSlider.value.ToString());
    }
    public void SetFrameLimit()
    {
        Application.targetFrameRate = (int)fpsSlider.value;
        fpsValue.SetText(fpsSlider.value.ToString());
    }
}
