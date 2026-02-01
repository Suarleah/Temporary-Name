using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSFX(Single volume)
    {
        soundManager.sfx.volume = volume;
    }
    public void ChangeMusic(Single volume)
    {
        soundManager.music.volume = volume;
    }
}
