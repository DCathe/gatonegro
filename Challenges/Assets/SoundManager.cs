using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip _gameOver;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverAudio()
    {
        _audioSource.PlayOneShot(_gameOver);
    }
}
