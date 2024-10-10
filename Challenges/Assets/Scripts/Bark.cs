using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bark : MonoBehaviour
{
    public AudioClip barkClip;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (barkClip != null)
        {
            _audioSource.clip = barkClip;
        }
    }
        
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _audioSource.Play();
        }
    }
}
