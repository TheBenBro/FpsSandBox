using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class HandleWindow : MonoBehaviour
{

    //private Resolution[] resolutions;
    Resolution[] resolutions;
    private int resolutionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
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
    }

    public void HandleWindowMode(int value_)
    {
        if (value_ == 0)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (value_ == 1)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (value_ == 2)
        { 
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}

