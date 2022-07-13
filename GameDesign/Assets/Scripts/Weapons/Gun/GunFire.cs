using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    public GameObject bulletPrefabs;
    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public Vector3 rotation;

    public float lifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {  
            if(MainManager.Instance.numberOfBullets > 0)
                 Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefabs);

        //   Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

        bullet.transform.position = bulletSpawn.position;

      //  rotation = bullet.transform.rotation.eulerAngles;
     //   bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));

        PlayerInventory pi = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        pi.BulletUsed();

    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
