using System;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    [SerializeField] CapsuleCollider _Collider;
    [SerializeField] float _Capsuleradius;

    private PickUpWeapon _HasWeapon;
    // Start is called before the first frame update
    void Start()
    {
        _Collider = gameObject.AddComponent<CapsuleCollider>();
        _Collider.radius = _Capsuleradius;
        _Collider.height = 6f;
        _Collider.isTrigger = true;
        _HasWeapon = GetComponent<PickUpWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_HasWeapon._hasWeapon)
        {
            if (_HasWeapon._currentWeapon.name == "Melee")
            {
                MeleeHit();
            }
            else if (_HasWeapon._currentWeapon.name == "Gun")
                GunHit();
            return;
            
        }
        else
                PunchHit();


        Debug.Log(_Collider.radius);
    }

    private void GunHit()
        => _Collider.radius = _Capsuleradius * 2f;

    private void PunchHit()
        => _Collider.radius = _Capsuleradius;

    private void MeleeHit() 
        => _Collider.radius = _Capsuleradius * 1.5f;

    private void OnTriggerStay(Collider other)
    {
         Debug.Log("stay");
        if (Input.GetMouseButtonDown(0))
            Debug.Log("punch");
        if (Input.GetMouseButtonUp(0) && other.gameObject.tag == "Enemy")
            //destroying bc i for the life of me cannot get out of the spaghetti code of the enemy script.
            Destroy(other.gameObject);
    }
}
