using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public float damage;
    public Rigidbody2D bulletPrefab;

    public void Fire(Vector2 direction)
    {
        Vector3 initialPos;
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                initialPos = new Vector3(transform.position.x + 1.5f, transform.position.y, 0);
            }
            else
            {
                initialPos = new Vector3(transform.position.x - 1.5f, transform.position.y, 0);
            }
        }
        else
        {
            if(direction.y < 0)
            {
                initialPos = new Vector3(transform.position.x, transform.position.y - 1.5f, 0);
            }
            else
            {
                initialPos = new Vector3(transform.position.x, transform.position.y + 1.5f, 0);
            }
        }
        Rigidbody2D bullet = Instantiate(bulletPrefab, initialPos, Quaternion.identity) as Rigidbody2D;

        if (direction.x == 0 && direction.y == 0)
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed);
        }
        else
        {
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);
        }
    }
}
