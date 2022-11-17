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

    private int doOnce = 0;
    RaycastHit hit;

    GameObject player;

    public bool hasWeapon = true;

    private float minDist = Mathf.Infinity;

    [SerializeField]
    private GameObject closestWeapon = null;

    [SerializeField]
    private float weaponPickSpeed = 15.0f;

    private void Start()
    {
        doOnce = 0;
    }

    void Update()
    {
        if (hasWeapon)
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
                    myVector = new Vector3(Random.Range(xMinValue, xMaxValue), 1.8f, Random.Range(zMinValue, zMaxValue));
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
        else
        {
            GameObject[] weapons;
            if (doOnce == 0)
            {
                doOnce++;

                weapons = GameObject.FindGameObjectsWithTag("Weapon");
                foreach (var weapon in weapons)
                {
                    float dist = Vector3.Distance(weapon.transform.position, transform.position);
                    if (dist < minDist)
                    {
                        closestWeapon = weapon;
                        minDist = dist;
                    }
                }
            }

            Vector3 closestWeaponPos = new Vector3(closestWeapon.transform.position.x, 1.8f, closestWeapon.transform.position.z);

            var step = weaponPickSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, closestWeaponPos, step);
            _direction = (closestWeapon.transform.position - transform.position).normalized;

            Vector3 lookDirection = new Vector3(_direction.x, 0, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 360);
            if (transform.position == closestWeaponPos)
            {
                hasWeapon = true;
            }

        }
        

    }
}
