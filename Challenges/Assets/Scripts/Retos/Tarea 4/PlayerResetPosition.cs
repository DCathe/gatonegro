using UnityEngine;

public class PlayerResetPosition : MonoBehaviour
{
    [SerializeField]
    private Transform _initialTransform;

    public void ResetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterController characterController = player.GetComponent<CharacterController>();
        characterController.enabled = false;
        player.transform.position = _initialTransform.position;
        characterController.enabled = true;
    }
}
