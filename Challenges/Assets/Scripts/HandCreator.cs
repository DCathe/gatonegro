using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BezierCurve))]
[RequireComponent(typeof(AudioSource))]
public class HandCreator : MonoBehaviour
{
    public UnityEvent CapturedOneCoin;
    public UnityEvent LaunchedGlove;
    public UnityEvent GloveArrived;

    [SerializeField]
    private GameObject _handPrefab;
    
    [Header("Target")]
    [SerializeField]
    private Transform _target;

    [SerializeField]
    [Range(1, 10)]
    private float _luanchDuration;

    [SerializeField]
    private float _distanceThreshold = 0.1f;

    [SerializeField]
    private Transform _spawnPoint;


    [Header("Comeback")]
    [SerializeField]
    private AnimationCurve _comebackCurve;

    [SerializeField]
    private Transform _comebackTarget;

    [SerializeField]
    [Range(1, 10)]
    private float _comebackDuration;

    [SerializeField]
    private float _tractionHelperAmount = 0.1f;

    private float _totalTractionHelperAmount = 0;

    private BezierCurve _curve;
    private Rigidbody _handRigidbody;

    private bool _isHandBack = true;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip _pickupClip;

    private AudioSource _audioSource;
    
    void Start()
    {
        if (_handPrefab == null)
        {
            throw new UnityException("No hand prefab have been selected");
        }
        if (_target == null)
        {
            throw new UnityException("No target have been setup");
        }
        if (_comebackTarget == null)
        {
            throw new UnityException("Not comeback target have been selected");
        }
        _curve = GetComponent<BezierCurve>();
        _handRigidbody = _handPrefab.GetComponent<Rigidbody>();
        _handPrefab.gameObject.SetActive(false);
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isHandBack)
        {
            _isHandBack = false;
            _handPrefab.transform.position = _spawnPoint.position;
            _handPrefab.gameObject.SetActive(true);
            StartCoroutine(MoveGloveToTarget());
        }
    }

    private IEnumerator MoveGloveToTarget()
    {
        LaunchedGlove.Invoke();
        _handPrefab.transform.SetParent(null);
        float elapsedTime = 0;

        while (elapsedTime < _luanchDuration)
        {
            float t = elapsedTime / _luanchDuration;
            Vector3 positionInCurve = _curve.CalculateQuadraticBezierPoint(t);
            _handRigidbody.MovePosition(positionInCurve);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _handRigidbody.position = _target.position;
        StartCoroutine(MoveGloveBackToPlayer());
    }

    private IEnumerator MoveGloveBackToPlayer()
    {
        float elapsedTime = 0;
        _totalTractionHelperAmount = 0;
        float distanceToTarget = Vector3.Distance(_handRigidbody.position, _comebackTarget.position);
        while (distanceToTarget > _distanceThreshold)
        {
            float t = elapsedTime / _comebackDuration;
            float curveValue = _comebackCurve.Evaluate(t);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _totalTractionHelperAmount += _tractionHelperAmount;
            }

            Vector3 newPosition = Vector3.Lerp(_target.position, _comebackTarget.position, curveValue + _totalTractionHelperAmount);
            _handRigidbody.MovePosition(newPosition);

            elapsedTime += Time.deltaTime;
            yield return null;

            distanceToTarget = Vector3.Distance(_handRigidbody.position, _comebackTarget.position);
        }

        _handRigidbody.position = _comebackTarget.position;
        DeletableHand[] deletableHands = _handPrefab.GetComponentsInChildren<DeletableHand>();
        foreach (DeletableHand deletableHand in deletableHands)
        {
            CapturedOneCoin.Invoke();
            Destroy(deletableHand.gameObject);
        }
        _isHandBack = true;
        _handPrefab.gameObject.SetActive(false);
        GloveArrived.Invoke();
    }

    public void PlayPickupClip()
    {
        _audioSource.PlayOneShot(_pickupClip);
    }
}
