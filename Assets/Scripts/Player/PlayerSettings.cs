using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSettings : MonoBehaviour, ISaveable
{
    private float mouseSensitivity = 50f;
    private bool showFPS = true;
    private bool vsync = false;
    private int targetFrameRate = 300;
    private int maxRooms = 25;

    private void Start()
    {
        SaveLoadSystem.Instance.Load();
    }
    public float GetMouseSensitivity()
    {
        return mouseSensitivity;
    }
    public bool GetShowFPS()
    {
        return showFPS;
    }
    public bool GetVSync()
    {
        return vsync;
    }
    public int GetTargetFrameRate()
    {
        return targetFrameRate;
    }
    public int GetMaxRooms()
    {
        return maxRooms;
    }

    public void SetMouseSensitivity(float mouseSensitivity_)
    {
        mouseSensitivity = mouseSensitivity_;
    }
    public void SetShowFPS(bool showFps_)
    {
        showFPS = showFps_;
    }
    public void SetVSync(bool vSync_)
    {
        vsync = vSync_;
    }
    public void SetTargetFrameRate(int FPS_)
    {
        targetFrameRate = FPS_;
    }
    public void SetMaxRooms(int maxRooms_)
    {
        maxRooms = maxRooms_;
    }

    public object SaveState()
    {
        return new SaveData()
        {
            mouseSensitivity = this.mouseSensitivity,
            showFPS = this.showFPS,
            vsync = this.vsync,
            maxRooms = this.maxRooms,
            targetFrameRate = this.targetFrameRate
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        mouseSensitivity = saveData.mouseSensitivity;
        showFPS = saveData.showFPS;
        vsync = saveData.vsync;
        maxRooms = saveData.maxRooms;
        targetFrameRate = saveData.targetFrameRate;
    }

    [Serializable]
    private struct SaveData
    {
        public float mouseSensitivity;
        public bool showFPS;
        public bool vsync;
        public int targetFrameRate;
        public int maxRooms;
    }
}
