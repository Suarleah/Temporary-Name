using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] soundEffects;
    public AudioClip[] musicTracks;
    public AudioClip[] uiSounds;

    public AudioSource sfx;
    public AudioSource music;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayMusic(musicTracks[0]); // Might need to be commented out later
    }


    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip, 1);
    }

    public void PlayMusic(AudioClip clip)
    {
        music.Stop();
        music.PlayOneShot(clip, 1);
    }
}
