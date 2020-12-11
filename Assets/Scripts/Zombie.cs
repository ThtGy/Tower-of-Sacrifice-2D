using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public Transform target, homePos;
    public float chaseRadius, attackRadius;
    public Animator animator;

    private Rigidbody2D rb;
    private AudioSource growl;
    private bool hasGrowled;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        growl = GetComponent<AudioSource>();
        target = GameObject.FindWithTag("Player").transform;
        hasGrowled = false;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            CheckInRange();
        }
        else
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    void CheckInRange()
    {
        //within chase range but outside of attack range
        if(Vector2.Distance(target.position, transform.position) <= chaseRadius 
            && Vector2.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector2 movement = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                rb.MovePosition(movement);

                Vector2 temp = movement;
                temp.x = movement.x - transform.position.x;
                temp.y = movement.y - transform.position.y;

                if (!hasGrowled && !growl.isPlaying)
                {
                    growl.Play();
                    hasGrowled = true;
                }

                ChangeAnimation(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else
        {
            animator.SetBool("Walking", false);
            if(Vector2.Distance(target.position, transform.position) > chaseRadius)
                hasGrowled = false;
        }

        //within attack range
        if(Vector2.Distance(target.position, transform.position) <= attackRadius)
        {
            if(currentState != EnemyState.attack && currentState != EnemyState.stagger)
            {
                StartCoroutine(AttackCo());
            }
        }
    }

    private void ChangeAnimation(Vector2 direction)
    {
        animator.SetBool("Walking", true);
        animator.SetFloat("Horizontal", direction.normalized.x);
        animator.SetFloat("Vertical", direction.normalized.y);
    }

    private void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        ChangeState(EnemyState.attack);
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(0.8f);
        ChangeState(EnemyState.idle);
    }
}
