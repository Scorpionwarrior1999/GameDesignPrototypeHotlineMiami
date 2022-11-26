using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private GameObject _player;

    private Scene _currentScene;
    private bool _playerIsDead = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if(_player == null)
        {
            _playerIsDead = true;
        }

        if(_playerIsDead)
        {
            FindAllAndDisable<EnemyPathfollowing>();
            FindAllAndDisable<EnemyWalkTowardsPlayer>();
            FindAllAndDisable<EnemyWandering>();

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
