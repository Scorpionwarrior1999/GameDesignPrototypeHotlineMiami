using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private GameObject _blood;


    private GameObject _player;

    private Scene _currentScene;
    private bool _playerIsDead = false;

    private bool _done = false;

    private Vector3 _playerPos;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (_player != null)
        {
            _playerPos = _player.transform.position;
        }


        if (_player == null)
        {
            _playerIsDead = true;
        }

        if (_playerIsDead)
        {
            FindAllAndDisable<EnemyPathfollowing>();
            FindAllAndDisable<EnemyWalkTowardsPlayer>();
            FindAllAndDisable<EnemyWandering>();

            if (!_done)
            {
                Instantiate(_blood, _playerPos + Vector3.up * 0.1f, Quaternion.identity);
                _done = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(_currentScene.name);
        }
    }

    private static void FindAllAndDisable<T>()
        where T : MonoBehaviour
    {
        var allObjectsArray = FindObjectsOfType<T>();
        foreach (T objectType in allObjectsArray)
            objectType.enabled = false;
    }
}
