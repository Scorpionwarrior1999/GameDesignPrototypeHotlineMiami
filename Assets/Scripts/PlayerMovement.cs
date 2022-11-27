using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _groundDrag, _acceleration, _maxSpeed;
    [SerializeField]
    private CharacterController _charCtrl;
    [SerializeField]
    private LayerMask _floorLayer,
        _defaultLayer;

    private Vector3 _velocity, _inputVector;

    private GameObject _enemy;


    void Update()
    {
        _inputVector += Input.GetAxis("Horizontal") * Vector3.right;
        _inputVector += Input.GetAxis("Vertical") * Vector3.forward;

        _charCtrl.Move(_velocity * Time.deltaTime);

        GetEnemy();

        if (_enemy != null)
            LookAtEnemy();
        else if (_enemy == null)
            LookAtMouse();
    }

    private void GetEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(2))
        {
            if(_enemy == null)
            {
                if (Physics.Raycast(ray, out var hit, 30, _defaultLayer))
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        _enemy = hit.transform.gameObject;
                    }
                }
            }

            else
            {
                _enemy = null;
            }

        }
    }

    private void LookAtMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 30, _floorLayer))
        {
            Vector3 tempDirection = hit.point;
            tempDirection.y = transform.position.y;

            transform.LookAt(tempDirection);
        }
    }

    private void LookAtEnemy()
    {
         Vector3 tempDirection = _enemy.transform.position;
  
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
