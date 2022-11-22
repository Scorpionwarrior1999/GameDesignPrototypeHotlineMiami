using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnparent : MonoBehaviour
{
    [SerializeField]
    public bool isMeleeWeapon = false;
    public bool isGun = false;

    private void Start()
    {
        int rnd = Random.Range(1, 20);
        if (rnd <= 10)
        {
            isMeleeWeapon = true;
        }
        else
        {
            isGun = true;
        }

    }

    void Update()
    {
        if (gameObject.transform.parent == null)
        {
            gameObject.tag = "Weapon";
        }
    }
}
