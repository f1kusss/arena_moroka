using UnityEngine;
public class MusicPlayer : MonoBehaviour

{

    public AudioClip musicClip;

    private AudioSource audioSource;
    void Start()

    {

        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicClip;

        audioSource.Play();

    }

}