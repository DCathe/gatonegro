using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public int speed = 5;
    public float gravity = 0.9f;

    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (!_controller.isGrounded)
        {
            movement.y = -gravity;
        }
        _controller.Move(movement * Time.deltaTime * speed);
    }
}
