using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    private SoundManager soundManager;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
