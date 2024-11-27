using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class FollowingCamera : MonoBehaviour
{
    [Header("Camera Traslation")]
    [SerializeField]
    private Transform _followingTransform;

    [SerializeField]
    private float _minimumThreshold = 0.1f;

    [SerializeField]
    private float _lerpTransitionTime = 1.0f;

    [SerializeField]
    private Vector3 _positionOffset = new Vector3(0, 0, -3);

    private Vector2 _look;
    
    [Header("Camera Rotation")]
    [SerializeField]
    [Tooltip("How far in degrees can you move the camera up")]
    private float _topClamp = 70.0f;

    [SerializeField]
    [Tooltip("How far in degrees can you move the camera down")]
    private float _bottomClamp = -30.0f;
    
    [SerializeField]
    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    private float _cameraAngleOverride = 0.0f;

    private const float _threshold = 0.01f;

    private float _cameraTargetYaw;
    private float _cameraTargetPitch;

#if ENABLE_INPUT_SYSTEM
    [Space]
    [SerializeField]
    [Tooltip("Player input used to know current control schema, normally in the player game object")]
    private PlayerInput _playerInput;
#endif

    private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


    private void Start()
    {
        if (_followingTransform == null)
        {
            throw new UnityException("No following transform selected");
        }
        if (_playerInput == null)
        {
            throw new UnityException("No player input selected");
        }
        _cameraTargetYaw = transform.rotation.eulerAngles.y;
    }
    
    private void Update()
    {
        float distance = Vector3.Distance(_followingTransform.position, transform.position);
        if (distance > _minimumThreshold)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _followingTransform.position, _lerpTransitionTime) + _positionOffset;
            transform.position = nextPosition;
        }   
    }

    private void LateUpdate()
    {        
        CameraRotation();  
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }

    private void CameraRotation()
    {
        if (_look.sqrMagnitude >= _threshold)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cameraTargetYaw += _look.x * deltaTimeMultiplier;
            _cameraTargetPitch += _look.y * deltaTimeMultiplier;
        }

        _cameraTargetYaw = ClampAngle(_cameraTargetYaw, float.MinValue, float.MaxValue);
        _cameraTargetPitch = ClampAngle(_cameraTargetPitch, _bottomClamp, _topClamp);

        transform.rotation = Quaternion.Euler(_cameraTargetPitch + _cameraAngleOverride, _cameraTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
