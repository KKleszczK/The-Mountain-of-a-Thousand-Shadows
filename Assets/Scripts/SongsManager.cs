using UnityEngine;

//Old Version Script
public class SongsManager : MonoBehaviour
{
    public AudioClip song1;  
    public AudioClip song2;  
    private AudioSource audioSource;

    private void Start()
    {
        
        audioSource = GetComponent<AudioSource>();

        PlaySong(song1);
    }

    private void PlaySong(AudioClip song)
    {
        audioSource.clip = song;  
        audioSource.Play();
    }

    
    private void Update()
    {
        
        if (!audioSource.isPlaying)
        {
            
            if (audioSource.clip == song1)
            {
                PlaySong(song2);
            }
        }
    }
}
