using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    [SerializeField] CapsuleCollider _Collider;
    [SerializeField] float _Capsuleradius;
    [SerializeField] GameObject _Bullet;
    [SerializeField] private GameObject _WeaponPlace;

    private PickUpWeapon _HasWeapon;
    [SerializeField] private bool _spawnBullet = false;
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
                MeleeHit();
            else if (_HasWeapon._currentWeapon.name == "ShotGun" && Input.GetMouseButtonUp(0))
            {
                _spawnBullet = true;
                ShootShotgun(_Bullet);
                GunHit();
            }
            else if (_HasWeapon._currentWeapon.name == "Gun" && Input.GetMouseButtonUp(0))
            {
                _spawnBullet = true;
                Shoot(_Bullet);
                GunHit();
            }
        }
        else
            PunchHit();

        //Debug.Log(_Collider.radius);
    }

    private void Shoot(GameObject bullet)
        => Instantiate(bullet, _WeaponPlace.transform);


    private void PunchHit()
        => _Collider.radius = _Capsuleradius;
    private void MeleeHit()
        => _Collider.radius = _Capsuleradius * 1.5f;

    private void GunHit()
        => _Collider.radius = _Capsuleradius * 2f;

    private void ShootShotgun(GameObject bullet)
    {
        Instantiate(bullet, _WeaponPlace.transform);
        Instantiate(bullet, _WeaponPlace.transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0) && other.gameObject.CompareTag("Enemy") && _spawnBullet == false)
            //destroying bc i for the life of me cannot get out of the spaghetti code of the enemy script.
            Destroy(other.gameObject);
    }
}
