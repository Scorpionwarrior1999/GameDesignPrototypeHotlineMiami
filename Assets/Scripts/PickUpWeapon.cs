using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _WeaponPlace;
    [SerializeField] private float _Thrust = 20f;

    [SerializeField] private bool _hasWeapon = false;
    private GameObject _currentWeapon;
    private void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {        
        if (other.gameObject.tag == "Weapon" && Input.GetMouseButtonUp(1))
        {
            if (!_hasWeapon)
            {
                _hasWeapon = true;
                WeaponPos(other.gameObject);
                _currentWeapon = other.gameObject;
            }
            if (_hasWeapon)
            {
                WeaponThrow(_currentWeapon);

                _currentWeapon = other.gameObject;
                WeaponPos(other.gameObject);
            }
        }

        Debug.Log(other.transform.tag);
    }

    private void WeaponThrow(GameObject gameObject)
    {
        Transform childToRemove = this.transform.Find(gameObject.name);
        childToRemove.parent = null;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _Thrust);
    }

    private void WeaponPos(GameObject gameObject)
    {
        gameObject.transform.position = _WeaponPlace.transform.position;
        gameObject.transform.rotation = _WeaponPlace.transform.rotation;
        gameObject.transform.parent = this.transform;
    }
}
