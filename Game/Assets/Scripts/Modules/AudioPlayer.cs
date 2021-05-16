using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    public void Play()
    {
        AudioSource.clip = AudioClip;
        if (AudioSource.clip != null)
        {
            AudioSource.Play();
        }
    }

    public void Stop()
    {
        AudioSource.Stop();
    }
}