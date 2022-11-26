using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    [SerializeField] private GameObject _WeaponPlace;
    [SerializeField] private float _Thrust = 20f;

    public bool _hasWeapon = false;
    public GameObject _currentWeapon;
    private void Update()
    {
        if (_currentWeapon == null)
            _hasWeapon = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon") && Input.GetMouseButtonDown(1) && other.gameObject != _currentWeapon)
        {
            if (!_hasWeapon)
            {
                WeaponPos(other.gameObject);
                _hasWeapon = true;
                return;
            }
            if (_hasWeapon)
            {                
                WeaponThrow(_currentWeapon);
                return;
            }
        }
        //Debug.Log(other.transform.tag);
    }

    private void WeaponThrow(GameObject go)
    {
        Transform childToRemove = this.transform.Find(go.name);
        childToRemove.parent = null;
        go.tag = "Weapon";
        //gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _Thrust);
        _hasWeapon = false;
        _currentWeapon = null;
        return;
    }

    private void WeaponPos(GameObject go)
    {
        go.transform.SetPositionAndRotation(_WeaponPlace.transform.position, _WeaponPlace.transform.rotation);
        go.transform.parent = this.transform;

        _currentWeapon = go;
        go.tag = "Untagged";
    }
}
