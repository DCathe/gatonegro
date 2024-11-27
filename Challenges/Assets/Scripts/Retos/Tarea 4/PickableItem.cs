using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PickableItem : MonoBehaviour
{
    public UnityEvent _pickedUp;

    [SerializeField]
    private AudioClip _pickUpAudio;

    [SerializeField]
    private MeshRenderer _bodyRenderer;

    [SerializeField]
    private Collider _collider;

    private AudioSource _audioSource;

    private void Start()
    {
        if (_pickUpAudio == null)
        {
            throw new UnityException("No audio for pick up have been selected");
        }
        if (_bodyRenderer == null)
        {
            throw new UnityException("No renderer selected");
        }
        if (_collider == null)
        {
            throw new UnityException("No collider selected");
        }
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnPickUp()
    {
        _audioSource.PlayOneShot(_pickUpAudio);
        _pickedUp.Invoke();
        _bodyRenderer.enabled = false;
        _collider.enabled = false;
        StartCoroutine(DisableAfterAudioFinish());
    }

    private IEnumerator DisableAfterAudioFinish()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
