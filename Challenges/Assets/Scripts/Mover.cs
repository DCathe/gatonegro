using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 1.0f;
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal == 0 && vertical == 0) 
        {
            return;
        } 
        Vector3 newPosition = new Vector3(transform.position.x + horizontal * Time.deltaTime * speed, transform.position.y + vertical * Time.deltaTime * speed, transform.position.z);

        transform.position = newPosition;
    }
}
