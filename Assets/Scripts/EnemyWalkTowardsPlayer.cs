using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkTowardsPlayer : MonoBehaviour
{

    [SerializeField]
    private float speed = 1.0f;
    RaycastHit hit;

    GameObject player;
    private Vector3 _direction;

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


        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        _direction = (player.transform.position - transform.position).normalized;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_direction), Time.deltaTime * 360);

    }
}
