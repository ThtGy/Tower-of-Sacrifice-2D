using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;
    public AudioSource HitSE;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("canHit"))
        {
            collision.GetComponent<Box>().Open();
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            
            if (hit != null)
            {
                Vector2 forceDirection = hit.transform.position - transform.position;
                forceDirection = forceDirection.normalized * thrust;
                hit.AddForce(forceDirection, ForceMode2D.Impulse);

                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger && !gameObject.CompareTag("FireSpell"))
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knockback(hit, knockTime, damage);
                }
                if(collision.gameObject.CompareTag("Player") && collision.isTrigger)
                {
                    if (collision.GetComponent<Player>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<Player>().currentState = PlayerState.stagger;
                        collision.GetComponent<Player>().Knockback(knockTime, damage);
                    }
                }
                if (HitSE != null)
                    HitSE.Play();
            }

            if(gameObject.CompareTag("Projectile"))
            {
                Destroy(gameObject, 0.3f);
            }
        }
    }


}
