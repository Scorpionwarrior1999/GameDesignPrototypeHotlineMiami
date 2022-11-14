using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalking : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.forward, Color.red);
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 100.0f))
        {
            Debug.Log("Found an object - tag: " + hit.collider.gameObject.tag.ToString());
        }
    }
}
