using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    public GameObject warpPrefab;
    public GameObject firePrefab;
    public float retreatRadius;
    public AudioSource snap, fire;

    private Animator animator;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= retreatRadius)
            {
                Warp();
            }
            //check if player is in boss room
            else if(target.position.x >= 39 && target.position.x <= 54
                && target.position.y >= 18 && target.position.y <= 33.5f)
            {
                SummonFire();
            }
        }
        else
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void SummonFire()
    {
        if (currentState == EnemyState.idle && GameObject.FindGameObjectsWithTag("FireSpell").Length == 0)
        {
            currentState = EnemyState.attack;

            StartCoroutine(CastCo());
        }
    }

    private void Warp()
    {
        if (currentState == EnemyState.idle)
        {
            currentState = EnemyState.walk;

            if (target.transform.position.y > transform.position.y)
            {
                animator.SetFloat("Vertical", 1f);
            }
            else
            {
                animator.SetFloat("Vertical", -1f);
            }
            StartCoroutine(SnapCo());
        }
    }

    private IEnumerator CastCo()
    {
        animator.SetBool("Spellcast", true);
        yield return null;
        animator.SetBool("Spellcast", false);
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(SpawnFireCo());

        currentState = EnemyState.idle;
    }

    private IEnumerator SpawnFireCo()
    {
        float maxX = 53.5f;
        float minX = 39.5f;
        float yVal = target.transform.position.y;

        if(!fire.isPlaying)
        {
            fire.Play();
        }
        
        for(float i = maxX; i >= minX; i--)
        {
            Vector3 spawnPos = new Vector3(i, yVal, 1);
            GameObject newFire = Instantiate(firePrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            Destroy(newFire, 1f);
        }
    }

    private IEnumerator SnapCo()
    {
        animator.SetBool("Warp", true);
        yield return null;
        animator.SetBool("Warp", false);
        yield return new WaitForSeconds(0.4f);
        snap.Play();
        StartCoroutine(WarpCo());
    }

    private IEnumerator WarpCo()
    {
        GameObject warpEffect = Instantiate(warpPrefab, transform.position, Quaternion.identity);
        float xPos = Random.Range(40, 53);
        float yPos = Random.Range(16, 28);
        yield return new WaitForSeconds(0.4f);
        //don't spawn on top of the player or in current position
        while(xPos == target.transform.position.x || yPos == target.transform.position.y
            || xPos == transform.position.x || yPos == transform.position.y)
        {
            xPos = Random.Range(40, 53);
            yPos = Random.Range(16, 28);
        }
        transform.position = new Vector3(xPos, yPos, 1);
        Destroy(warpEffect, 0.4f);

        currentState = EnemyState.idle;
    }
}
