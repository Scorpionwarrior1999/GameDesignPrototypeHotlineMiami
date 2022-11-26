using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private float _cameraSpeed = 10;

    [SerializeField]
    private float _maxCenterPlayerDelay = 3, 
        _cameraShakeAmount = 2;

    private float _centerPlayerDelay;

    private Vector3 _cameraPosition = new Vector3(0, 12.5f, 0);
    private Vector3 _cameraRotation = new Vector3(90, 0, 0);


    void Start()
    {
        _player = FindObjectOfType<PlayerMovement>().gameObject;
        _centerPlayerDelay = _maxCenterPlayerDelay;

        transform.position = _cameraPosition;
        transform.rotation = Quaternion.Euler(_cameraRotation);
    }

    void Update()
    {
        _centerPlayerDelay -= Time.deltaTime;

        FollowPlayer();
        CameraShake();
        //CenterPlayer();
    }

    private void CenterPlayer()
    {
        Vector3 playerPos = _player.transform.position;
        playerPos.y = transform.position.y;
        if (IsPlayerOffCenter() && DelayIsZero())
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, _cameraSpeed * Time.deltaTime);
            transform.position -= _cameraSpeed * Time.deltaTime * Vector3.right;

        }
    }

    private bool DelayIsZero()
    {
        if (_centerPlayerDelay <= 0)
            return true;
        
        else
            return false;
    }

    private bool IsPlayerOffCenter()
    {
        Vector3 playerViewPos = Camera.main.WorldToViewportPoint(_player.transform.position);

        if (playerViewPos.x < 0.48f || 
            playerViewPos.x > 0.52f || 
            playerViewPos.y < 0.48f || 
            playerViewPos.y > 0.52f)
            return true;

        else 
            return false;

    }

    private void CameraShake()
    {
        Vector3 shake = Random.insideUnitSphere * _cameraShakeAmount;
        shake.y = 0;
        transform.position += shake * Time.deltaTime;
    }

    private void FollowPlayer()
    {
        if(_player != null)
        {
            Vector3 playerViewPos = Camera.main.WorldToViewportPoint(_player.transform.position);

            if (playerViewPos.x < 0.45f)
            {
                _centerPlayerDelay = _maxCenterPlayerDelay;
                transform.position -= _cameraSpeed * Time.deltaTime * Vector3.right;
            }

            else if (playerViewPos.x > 0.55f)
            {
                _centerPlayerDelay = _maxCenterPlayerDelay;
                transform.position += _cameraSpeed * Time.deltaTime * Vector3.right;
            }

            if (playerViewPos.y < 0.45f)
            {
                _centerPlayerDelay = _maxCenterPlayerDelay;
                transform.position -= _cameraSpeed * Time.deltaTime * Vector3.forward;
            }

            else if (playerViewPos.y > 0.55f)
            {
                _centerPlayerDelay = _maxCenterPlayerDelay;
                transform.position += _cameraSpeed * Time.deltaTime * Vector3.forward;
            }
        }
        
    }
}
