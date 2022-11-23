using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _WeaponPlace;
    [SerializeField] private float _Thrust = 20f;

    public bool _hasWeapon = false;
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

        //Debug.Log(other.transform.tag);
    }

    private void WeaponThrow(GameObject gameObject)
    {
        Transform childToRemove = this.transform.Find(gameObject.name);
        childToRemove.parent = null;
        gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _Thrust);
        _hasWeapon = false;
    }

    private void WeaponPos(GameObject gameObject)
    {
        _hasWeapon = true;
        gameObject.transform.parent = this.transform;
        gameObject.transform.position = _WeaponPlace.transform.position;
        gameObject.transform.rotation = _WeaponPlace.transform.rotation;
    }
}
