using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] soundEffects;
    public AudioClip[] musicTracks;
    public AudioClip[] uiSounds;

    private AudioSource sfx;
    
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    
    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip, 1);
    }
}
