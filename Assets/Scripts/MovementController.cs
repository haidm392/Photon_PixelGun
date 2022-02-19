using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private GameObject _fpsCamera;
    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private float _cameraUpDownRotation = 0f;
    private float _currentCameraUpAndDownRotation = 0f;

    private Rigidbody _rb;

    private float _lookSensitive = 3.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMovementInput = Input.GetAxis("Horizontal");
        float zMovementInput = Input.GetAxis("Vertical");

        Vector3 horizontalMovement = transform.right * xMovementInput;
        Vector3 verticalMovement = transform.forward * zMovementInput;

        Vector3 movementVelocity = (horizontalMovement + verticalMovement).normalized * _speed;

        Move(movementVelocity);

        float yRotation = Input.GetAxis("Mouse X");
        Vector3 rotationVector = new Vector3(0, yRotation, 0) * _lookSensitive;

        Rotate(rotationVector);
        
        //Calculate lookup and down camera rotation
        float cameraUpDownRotation = Input.GetAxis("Mouse Y") * _lookSensitive;
        
        //Apply rotation
        RotateCamera(cameraUpDownRotation);
    }
    private void FixedUpdate()
    {
        if (_velocity != Vector3.zero)
        {
            _rb.MovePosition(_rb.position + _velocity*Time.fixedDeltaTime);
        }
        
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation));

        if (_fpsCamera)
        {

            _currentCameraUpAndDownRotation -= _cameraUpDownRotation;
            _currentCameraUpAndDownRotation = Mathf.Clamp(_currentCameraUpAndDownRotation, -85, 85);
            _fpsCamera.transform.localEulerAngles = new Vector3(_currentCameraUpAndDownRotation, 0f, 0f);
        }
    }

    private void Rotate(Vector3 rotationVector)
    {
        _rotation = rotationVector;
    }

    private void RotateCamera(float cameraUpDownRotation)
    {
        _cameraUpDownRotation = cameraUpDownRotation;
    }


    private void Move(Vector3 movementVelocity)
    {
        _velocity = movementVelocity;
    }
}
