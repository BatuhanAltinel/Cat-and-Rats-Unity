using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip catPainSound;
    public AudioClip ratTrapSound;
    public AudioClip heartUpSound;

    public static SoundManager soundManager;
    
   
    void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void CatPainSound()
    {
        audioSource.PlayOneShot(catPainSound); 
    }
    public void RatTrapSound()
    {
        audioSource.PlayOneShot(ratTrapSound);
    }
    public void StopTheMusic()
    {
        audioSource.Stop();
    }
    public void StartTheMusic()
    {
        audioSource.Play();
    }
    public void HeartUpSound()
    {
        audioSource.PlayOneShot(heartUpSound);
    }
}
