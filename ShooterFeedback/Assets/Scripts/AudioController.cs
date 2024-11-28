using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource mSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] public AudioClip[] audios;

    public void playSFX(AudioClip clip)
    {
       sfxSource.PlayOneShot(clip);
    }

    public void playMusic(AudioClip clip)
    {
        mSource.clip = clip;
        mSource.Play();
    }
}
