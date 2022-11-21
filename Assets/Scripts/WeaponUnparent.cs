using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnparent : MonoBehaviour
{

    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            gameObject.tag = "Weapon";
        }
    }
}
