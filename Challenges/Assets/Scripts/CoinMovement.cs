using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoinMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Transform _moveDirection;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        if (_moveDirection == null)
        {
            throw new UnityException("No direction have been setup");            
        }
    }

    void FixedUpdate()
    {
        Vector3 newPosition = _rigidbody.position + _speed * Time.fixedDeltaTime * _moveDirection.forward;
        _rigidbody.MovePosition(newPosition);
    }
}
