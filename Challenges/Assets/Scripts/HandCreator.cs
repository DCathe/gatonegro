using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BezierCurve))]
public class HandCreator : MonoBehaviour
{
    public UnityEvent CapturedOneCoin;

    [SerializeField]
    private GameObject _handPrefab;
    
    [Header("Target")]
    [SerializeField]
    private Transform _target;

    [SerializeField]
    [Range(1, 10)]
    private float _luanchDuration;


    [Header("Comeback")]
    [SerializeField]
    private AnimationCurve _comebackCurve;

    [SerializeField]
    private Transform _comebackTarget;

    [SerializeField]
    [Range(1, 10)]
    private float _comebackDuration;

    private BezierCurve _curve;
    private Rigidbody _handRigidbody;

    private bool _isHandBack = true;
    
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isHandBack)
        {
            _isHandBack = false;
            _handPrefab.transform.position = transform.position;
            _handPrefab.gameObject.SetActive(true);
            StartCoroutine(MoveGloveToTarget());
        }
    }

    private IEnumerator MoveGloveToTarget()
    {
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

        while (elapsedTime < _comebackDuration)
        {
            float t = elapsedTime / _comebackDuration;
            float curveValue = _comebackCurve.Evaluate(t);

            Vector3 newPosition = Vector3.Lerp(_target.position, _comebackTarget.position, curveValue);
            _handRigidbody.MovePosition(newPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _handRigidbody.position = _comebackTarget.position;
        DeletableHand deletableHand = _handPrefab.GetComponentInChildren<DeletableHand>();
        if (deletableHand != null)
        {
            CapturedOneCoin.Invoke();
            Destroy(deletableHand.gameObject);
        }
        _isHandBack = true;
        _handPrefab.gameObject.SetActive(false);
    }
}
