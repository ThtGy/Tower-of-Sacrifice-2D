using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffectPrefab;

    private float health;

    void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void Knockback(Rigidbody2D enemyRB, float knockTime, float damage)
    {
        StartCoroutine(KnockCoroutine(enemyRB, knockTime));
        ReceiveDamage(damage);
    }

    private void ReceiveDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            DeathEffect();
            gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        if(deathEffectPrefab != null)
        {
            GameObject deathEffect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(deathEffect, 1f);
        }
    }

    private IEnumerator KnockCoroutine(Rigidbody2D enemyRB, float knockTime)
    {
        if (enemyRB != null)
        {
            yield return new WaitForSeconds(1f * knockTime);

            enemyRB.velocity = Vector2.zero;
            currentState = EnemyState.idle;
        }
    }
}
