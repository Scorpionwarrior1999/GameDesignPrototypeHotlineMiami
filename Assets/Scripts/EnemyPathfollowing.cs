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

    public bool hasWeapon = true;

    [SerializeField]
    private float weaponPickSpeed = 15.0f;

    //public int doOnce = 0;

    [SerializeField]
    private bool hasMeleeWeapon = false;

    [SerializeField]
    private bool hasGun = true;

    [SerializeField]
    private GameObject bullet;
    private int doOnce = 0;

    [SerializeField]
    private float minDistPlayer = 15f;

    private void Start()
    {
        wanderPoints = GameObject.FindGameObjectsWithTag("PathPoint");
        //doOnce = 0;
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
                if (hasMeleeWeapon)
                {
                    hasGun = false;
                    float minDistPlayer = 3f;
                    float dist = Vector3.Distance(player.transform.position, transform.position);
                    if (dist < minDistPlayer)
                    {
                        //player.gameObject.SetActive(false);
                        Destroy(player);
                    }
                }
                else if (hasGun)
                {
                    hasMeleeWeapon = false;
                    float dist = Vector3.Distance(player.transform.position, transform.position);
                    if (dist < minDistPlayer)
                    {
                        if (doOnce == 0)
                        {
                            doOnce++;
                            Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
                            StartCoroutine(spawnBullet());
                        }
                    }
                }
            }
        }
        else
        {
            hasGun = false;
            hasMeleeWeapon = false;
            float minDist = Mathf.Infinity;
            GameObject closestWeapon = null;
            GameObject[] weapons;
            weapons = GameObject.FindGameObjectsWithTag("Weapon");
            foreach (var weapon in weapons)
            {
                float dist = Vector3.Distance(weapon.transform.position, transform.position);
                if (dist < minDist)
                {
                    Debug.Log("closest weapon found");
                    closestWeapon = weapon;
                    minDist = dist;
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
                if (closestWeapon.GetComponent<WeaponUnparent>().isGun == true)
                {
                    hasGun = true;
                }
                else if (closestWeapon.GetComponent<WeaponUnparent>().isMeleeWeapon == true)
                {
                    hasMeleeWeapon = true;
                }
                closestWeapon.transform.parent = gameObject.transform;
                closestWeapon.transform.position = new Vector3(closestWeapon.transform.position.x, 1.8f, closestWeapon.transform.position.z);
                if (closestWeapon.transform.parent.tag == "Enemy")
                {
                    closestWeapon.tag = "Untagged";
                }
                //Destroy(closestWeapon);
            }
        }

    }

    private IEnumerator spawnBullet()
    {

        yield return new WaitForSeconds(0.25f);
        Debug.Log("a second has passed");
        doOnce = 0;

    }
}
