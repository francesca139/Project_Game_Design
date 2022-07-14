using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    public GameObject bulletPrefabs;
    public GameObject elixirPrefabs;

    public Transform bulletSpawn;
    public float bulletSpeed = 30f;
    public Vector3 rotation;

    public float lifeTime = 3f;
    public PlayerInventory pl;

    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MainManager.Instance.numberOfBullets > 0)
            Debug.Log("you can fire");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Fire();
            Debug.Log("Fire?");
        }
    }

    private void Fire()
    {
        if (MainManager.Instance.typeProiettile == 0)
        {
            if (MainManager.Instance.numberOfBullets > 0)
            {
                GameObject bullet = Instantiate(bulletPrefabs);

                //   Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

                bullet.transform.position = bulletSpawn.position;

                rotation = bullet.transform.rotation.eulerAngles;
                bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

                StartCoroutine(DestroyProiettileAfterTime(bullet, lifeTime));
            }

        }
        else if (MainManager.Instance.typeProiettile == 1)
        {
            if (MainManager.Instance.numberOfElixirs > 0)
            {
                GameObject elixir = Instantiate(elixirPrefabs);

                //   Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawn.parent.GetComponent<Collider>());

                elixir.transform.position = bulletSpawn.position;

                rotation = elixir.transform.rotation.eulerAngles;
                elixir.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

                elixir.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

                StartCoroutine(DestroyProiettileAfterTime(elixir, lifeTime));

                pl.ElixirUsed();
            }
        }

    }

    private IEnumerator DestroyProiettileAfterTime(GameObject proiettile, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(proiettile);
    }
}
