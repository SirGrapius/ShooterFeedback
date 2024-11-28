using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource mSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] public AudioClip[] audios;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
