using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfollowing : MonoBehaviour
{

    [SerializeField]
    public GameObject[] wanderPoints;

    private int _counter;

    private Vector3 _direction;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float pointSpeed = 1.0f;



    RaycastHit hit;

    GameObject player;

    private void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("PathPoint");
    }

    void Update()
    {

        Debug.DrawRay(transform.position, transform.forward, Color.red);
        var ray = Physics.Raycast(transform.position, transform.forward, out hit);
        if (ray)
        {
            Debug.Log("Found an object - tag: " + hit.collider.gameObject.tag.ToString());
            if (hit.collider.gameObject.tag == "Player")
            {
                player = hit.collider.gameObject;
            }

        }



        if (player == null)
        {
            Debug.Log(_counter);
            var step = pointSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, wanderPoints[_counter].transform.position, step);
            _direction = (wanderPoints[_counter].transform.position - transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 360);

            if (transform.position.x == wanderPoints[_counter].transform.position.x && transform.position.z == wanderPoints[_counter].transform.position.z)
            {
                if (_counter == wanderPoints.Length - 1)
                {
                    _counter = 0;
                }
                else
                {
                    _counter++;
                }

            }
        }
        else
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            _direction = (player.transform.position - transform.position).normalized;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 360);
        }
        
    }
}
