using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float _maxAngle = 120;


    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = Vector3.zero;
    }

    private void Update()
    {
        float currentYRotation = transform.rotation.eulerAngles.y;

        if(currentYRotation > 180 && currentYRotation < 360)
        {
            if(currentYRotation < 360 - _maxAngle)
            {
                currentYRotation = Mathf.Clamp(currentYRotation, 360 - _maxAngle, 360);
                _rb.angularVelocity = Vector3.zero;
            }
        }

        else if(currentYRotation > 0 && currentYRotation < 180)
        {
            if (currentYRotation > _maxAngle)
            {
                currentYRotation = Mathf.Clamp(currentYRotation, 0, _maxAngle);
                _rb.angularVelocity = Vector3.zero;
            }
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, currentYRotation, 0));
    }

}
