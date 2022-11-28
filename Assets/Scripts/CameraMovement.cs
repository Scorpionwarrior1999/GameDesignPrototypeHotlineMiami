using UnityEngine;
using Random = UnityEngine.Random;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _cameraSpeed = 10;

    [SerializeField]
    private float _maxCenterPlayerDelay = 3,
        _cameraShakeAmount = 2,
        _maxCameraTiltAngle = 3;

    private float _centerPlayerDelay;

    private GameObject _player;

    private Vector3 _cameraPosition = new Vector3(0, 12.5f, 0);
    private Vector3 _cameraRotation = new Vector3(90, 0, 0);

    private bool _isCameraUnlocked;


    void Start()
    {
        _cameraPosition.y = transform.position.y;
        _player = FindObjectOfType<PlayerMovement>().gameObject;
        _centerPlayerDelay = _maxCenterPlayerDelay;

        
        transform.rotation = Quaternion.Euler(_cameraRotation);
    }

    void Update()
    {
        _centerPlayerDelay -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isCameraUnlocked = true;
        }
        else
        {
            _isCameraUnlocked = false;
        }


        UnlockCameraControl();
        FollowPlayer();
        CameraTilt();
        CameraShake();
        //CenterPlayer();
    }

    private void UnlockCameraControl()
    {
        if (_isCameraUnlocked)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = _cameraPosition.y;
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 halfwayPosition = (mouseWorldPosition - _player.transform.position) / 2;
            halfwayPosition += _player.transform.position;
            halfwayPosition.y = _cameraPosition.y;

            this.transform.position = halfwayPosition;
        }
    }

    private void CameraTilt()
    {
        if (_player != null)
        {
            Vector3 playerPosition = _player.transform.position;

            float tiltAngle = 0;

            if (playerPosition.x < 0)
            {
                tiltAngle = -playerPosition.x * 0.1f;
                tiltAngle = Mathf.Clamp(tiltAngle, 0, _maxCameraTiltAngle);
            }

            else if (playerPosition.x > 0)
            {
                tiltAngle = -playerPosition.x * 0.1f;
                tiltAngle = Mathf.Clamp(tiltAngle, -_maxCameraTiltAngle, 0);
            }

            transform.rotation = Quaternion.Euler(new Vector3(_cameraRotation.x, tiltAngle, 0));
        }

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
        if (!_isCameraUnlocked)
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
        if (!_isCameraUnlocked)
        {
            if (_player != null)
            {
                Vector3 playerViewPos = Camera.main.WorldToViewportPoint(_player.transform.position);
                _centerPlayerDelay = _maxCenterPlayerDelay;

                if (playerViewPos.x < 0.45f)
                {
                    transform.position -= _cameraSpeed * Time.deltaTime * Vector3.right;
                }

                else if (playerViewPos.x > 0.55f)
                {
                    transform.position += _cameraSpeed * Time.deltaTime * Vector3.right;
                }

                if (playerViewPos.y < 0.45f)
                {
                    transform.position -= _cameraSpeed * Time.deltaTime * Vector3.forward;
                }

                else if (playerViewPos.y > 0.55f)
                {
                    transform.position += _cameraSpeed * Time.deltaTime * Vector3.forward;
                }
            }
        }

    }
}
