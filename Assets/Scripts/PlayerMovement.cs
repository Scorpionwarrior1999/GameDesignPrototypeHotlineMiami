using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _groundDrag, _acceleration, _maxSpeed;
    [SerializeField]
    private CharacterController _charCtrl;
    [SerializeField]
    private LayerMask _floorLayer;

    private Vector3 _velocity, _inputVector;


    void Update()
    {
        _inputVector += Input.GetAxis("Horizontal") * Vector3.right;
        _inputVector += Input.GetAxis("Vertical") * Vector3.forward;

        _charCtrl.Move(_velocity * Time.deltaTime);

        //LookAtDirection();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        //Vector3 mousePos = Camera.main.ViewportToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 30, _floorLayer))
        {
            Vector3 tempDirection = hit.point;
            tempDirection.y = transform.position.y;

            transform.LookAt(tempDirection);
        }
    }

    private void LookAtDirection()
    {
        Vector3 tempDirection = transform.position + _velocity;
        tempDirection.y = transform.position.y;

        transform.LookAt(tempDirection);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        ApplyMovement();
        ApplyGroundDrag();
        LimitMovement();

    }

    private void LimitMovement()
    {
        float tempY = _velocity.y;
        _velocity.y = 0;
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);
        _velocity.y = tempY;

    }

    private void ApplyGroundDrag()
    {
        if (_charCtrl.isGrounded)
        {
            _velocity *= (1 - Time.deltaTime * _groundDrag);
        }
    }

    private void ApplyMovement()
    {
        _velocity += _inputVector * _acceleration;
        _inputVector = Vector3.zero;
    }

    private void ApplyGravity()
    {
        if (_charCtrl.isGrounded)
        {
            _velocity.y = Physics.gravity.y * _charCtrl.skinWidth;
        }
        else
            _velocity.y += Physics.gravity.y * Time.deltaTime;
    }
}
