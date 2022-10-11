using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
public class HandleWindow : MonoBehaviour,ISaveable
{

    //private Resolution[] resolutions;
    Resolution[] resolutions;
    private int resolutionIndex = 0;

    private int currentHeight;
    private int currentWidth;
    private FullScreenMode currentFullScreenMode;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        SaveLoadSystem.Instance.Load();

    }

    public void SetResolution(TMP_Text text_)
    {
        text_.SetText(currentWidth.ToString() + "x" + currentHeight.ToString());
        Screen.SetResolution(currentWidth, currentHeight, Screen.fullScreenMode, 0);
        Screen.fullScreenMode = currentFullScreenMode;
    }
    public void IncreaseResolution(TMP_Text text_)
    {
        resolutionIndex++;
        if(resolutionIndex > resolutions.Length -1)
        {
            resolutionIndex = 0;
        }
        text_.SetText(resolutions[resolutionIndex].width.ToString() + "x" + resolutions[resolutionIndex].height.ToString());
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height,Screen.fullScreenMode,0);
        currentHeight = resolutions[resolutionIndex].height;
        currentWidth = resolutions[resolutionIndex].width;
        
    }
    public void DecreaseResolution(TMP_Text text_)
    {
        resolutionIndex--;
        if (resolutionIndex < 0)
        {
            resolutionIndex = resolutions.Length - 1;
        }
        text_.SetText(resolutions[resolutionIndex].width.ToString() + "x" + resolutions[resolutionIndex].height.ToString());
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, Screen.fullScreenMode,0);
        currentHeight = resolutions[resolutionIndex].height;
        currentWidth = resolutions[resolutionIndex].width;
    }

    public void HandleWindowMode(int value_)
    {
        if (value_ == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            currentFullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (value_ == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            currentFullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (value_ == 2)
        { 
            Screen.fullScreenMode = FullScreenMode.Windowed;
            currentFullScreenMode = FullScreenMode.Windowed;
        }
    }
    public object SaveState()
    {
        return new SaveData()
        {
            currentHeight = this.currentHeight,
            currentWidth = this.currentWidth, 
            //currentFullScreenMode = this.currentFullScreenMode,
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        currentHeight = saveData.currentHeight;
        currentWidth = saveData.currentWidth;
        //currentFullScreenMode = saveData.currentFullScreenMode;
    }

    [Serializable]
    private struct SaveData
    {
        public int currentHeight;
        public int currentWidth;
        //public FullScreenMode currentFullScreenMode;
    }
}

