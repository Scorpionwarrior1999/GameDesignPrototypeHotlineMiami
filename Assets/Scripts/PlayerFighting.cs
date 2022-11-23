using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    [SerializeField] PickUpWeapon _HasWeapon;

    private bool _canHit;
    // Start is called before the first frame update
    void Start()
    {
        _HasWeapon = GetComponent<PickUpWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_HasWeapon._hasWeapon)
        {
            //gun code
            _canHit = false;
        }
        else
            _canHit = true;
    }
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
