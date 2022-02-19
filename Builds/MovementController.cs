using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;

    private Vector3 _velocity;

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate movement veloc, ty as a 3d vector
        float _xMovement = Input.GetAxis("Horizontal");
        float _zMovement = Input.GetAxis("Vertical");


        Vector3 _movementHorizontal = transform.right * _xMovement;
        Vector3 _movementVertical = transform.forward * _zMovement;
        
        //Final movement velocity
        Vector3 _movementVelocity = (_movementHorizontal + _movementVertical).normalized * _speed;
        
        //Apply movement
        Move(_movementVelocity);
        
        //calculate rotation as a 3D vector for turning around
        float _yRotation = In
    }

    private void FixedUpdate()
    {
        if (_velocity != Vector3.zero)
        {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
        }
    }

    private void Move(Vector3 movementVelocity)
    {
        _velocity = movementVelocity;
    }
}
