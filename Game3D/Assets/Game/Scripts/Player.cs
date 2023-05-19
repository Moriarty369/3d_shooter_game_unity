
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _playerSpeed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        playerMovement();
    }
    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        _controller.Move(direction * Time.deltaTime);
        Vector3 velocity = direction * _playerSpeed;
        velocity.y -= _gravity;
        // Override velocity to allocate the global rotation to our player
        velocity = transform.transform.TransformDirection(velocity); 
        _controller.Move (velocity * Time.deltaTime);
    }
}
