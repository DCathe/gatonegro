using System.Collections;
using UnityEngine;

public class ChangeSizeAnimation : MonoBehaviour
{
    private Vector3 _finalSize;
    private Vector3 _initialSize;

    [SerializeField]
    private float _transitionTime = 0.70f;

    [SerializeField]
    private float _offsetMaxValue = 10.0f;

    [SerializeField]
    private float _maxScale = 0.5f;

    private float _offset;
    private void Start()
    {
        _initialSize = transform.localScale;
        _finalSize = _initialSize * _maxScale;
        _offset = Random.value * _offsetMaxValue;  
    }

    private void Update()
    {
        float progress = Mathf.PingPong(Time.time + _offset/ _transitionTime, 1f);
        transform.localScale = Vector3.Lerp(_initialSize, _finalSize, progress);
    }
}
