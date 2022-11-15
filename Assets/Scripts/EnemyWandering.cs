using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWandering : MonoBehaviour
{

    private Vector3 _direction;

    [SerializeField]
    private float speed = 1.0f;

    [SerializeField]
    private float wanderSpeed = 1.0f;


    [SerializeField]
    private float xMaxValue = 1.0f;

    [SerializeField]
    private float zMaxValue = 1.0f;
    
    [SerializeField]
    private float xMinValue = 1.0f;

    [SerializeField]
    private float zMinValue = 1.0f;

    [SerializeField]
    private float timeLeft = 0;

    Vector3 myVector;


    RaycastHit hit;

    GameObject player;

    private void Start()
    {
        
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

            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                myVector = new Vector3(Random.Range(xMinValue, xMaxValue), 1.8f , Random.Range(zMinValue, zMaxValue));
                Debug.Log(myVector.ToString());
                timeLeft = 5;
            }
            else
            {
                var step = wanderSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, myVector, step);
                _direction = (myVector - transform.position).normalized;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 360);
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
