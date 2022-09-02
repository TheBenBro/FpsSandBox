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
    public Slider sensSlider;
    public Toggle fpsToggle;
    private void Start()
    {
        sensSlider.value = GameManager.Instance.playerSettings.mouseSensitivity;
        GameManager.Instance.playerSettings.showFPS = fpsToggle.isOn;
        sensValue.SetText("100");
        
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
}
