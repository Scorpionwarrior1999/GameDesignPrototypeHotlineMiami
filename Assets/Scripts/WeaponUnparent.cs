using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnparent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            gameObject.tag = "Weapon";
        }
    }
}
