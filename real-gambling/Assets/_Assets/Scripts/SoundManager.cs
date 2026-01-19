using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioClip[] media;
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //PlayIntroAndLoop();
    }

    public void PlaySound(int sound)
    {
        source.PlayOneShot(media[sound]);
    }

    public void PlayIntroAndLoop()
    {
        if(!source.isPlaying)
        {
            float delayLength = media[0].length;
            source.PlayOneShot(media[0]);
            source.clip = media[1];
            source.loop = true;
            source.PlayDelayed(delayLength);
        }
    }

    public void StopSong()
    {
        source.Stop();
    }
}
