using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private int _BulletSpeed = 1000;
    [SerializeField] private float _MaxSpread;

    // Update is called once per frame
    private void Start()
    {
        if(this.transform.parent.name == "ShotGun")
            FireShotgun();
        else
            Fireriffle();
    }

    private void Fireriffle()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * _BulletSpeed);
        this.transform.parent = null;
    }

    private void FireShotgun()
    {
        Vector3 dir = transform.forward
            + new Vector3(Random.Range(-_MaxSpread, _MaxSpread), 1, 1);
        this.GetComponent<Rigidbody>().AddForce(dir * _BulletSpeed);
        this.transform.parent = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            return;
        }
    }
}
