using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletDist = 0.2f;

    [SerializeField]
    private int bulletSpeed = 1000;
    RaycastHit hit;

    // Update is called once per frame

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * bulletSpeed);
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        var ray = Physics.Raycast(transform.position, transform.forward, out hit, bulletDist);
        if (ray)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("Found an object player");
                Destroy(hit.collider.gameObject);
            }

        }
        //ShotEffect();
        //Destroy(gameObject);
    }


    private IEnumerator ShotEffect()
    {
        yield return 1;

    }
}
