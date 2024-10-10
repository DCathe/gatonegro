using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Jump : MonoBehaviour
{    
    private Animator _animator;

    private int _jumpTriggerHash = Animator.StringToHash("jump");

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _animator.SetTrigger(_jumpTriggerHash);
        }
    }
}
