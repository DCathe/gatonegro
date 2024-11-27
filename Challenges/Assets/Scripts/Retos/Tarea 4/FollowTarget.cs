using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _stoppingDistance = 3.0f;

    [SerializeField]
    private Animator _animator;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_animator == null)
        {
            throw new UnityException("No animator selected");
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        float normalizedSpeed = Mathf.Min(1.0f, _agent.velocity.magnitude / _agent.speed);
        _animator.SetFloat("Speed", normalizedSpeed);
        
        float distance = Vector3.Distance(_target.position, transform.position);
        if (distance > _stoppingDistance)
        {            
            _agent.SetDestination(_target.position);
        }
    }
}
