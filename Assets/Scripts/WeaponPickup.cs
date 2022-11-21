using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Collider _pickUprange;
    [SerializeField] private GameObject _WeaponPlace;
    [SerializeField] private float _Thrust = 20f;
    private bool _hasWeapon = false;
    private GameObject _currentWeapon;
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag == "Weapon" && Input.GetMouseButtonUp(1))
        {
            if (_hasWeapon == true)
            {
                Transform childToRemove = this.transform.Find(_currentWeapon.name);
                childToRemove.parent = null;
                //_currentWeapon.GetComponent<Rigidbody>().AddForce(_currentWeapon.transform.forward * _Thrust);

                _hasWeapon = true;

                _currentWeapon = other.gameObject;
                Debug.Log("true");
                WeaponPos(other);
            }
            else
            {
                _hasWeapon = true;

                _currentWeapon = other.gameObject;
                Debug.Log("true");
                WeaponPos(other);
            }
        }
    }

    private void WeaponPos(Collider other)
    {
        other.transform.position = _WeaponPlace.transform.position;
        other.transform.rotation = _WeaponPlace.transform.rotation;
        other.transform.parent = this.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
