using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger
}

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public PlayerState currentState;
    public AudioSource step1, step2;
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    public Vector2 prevDirection;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private CurrentPlayer currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPlayer = GetComponentInParent<PlayerSwitch>().currentCharacter;
        prevDirection = GetComponentInParent<PlayerSwitch>().prevDirection;

        if (prevDirection != null)
        {
            animator.SetFloat("Horizontal", prevDirection.x);
            animator.SetFloat("Vertical", prevDirection.y);
        }
        else
        {
            animator.SetFloat("Vertical", 1);
        }
    }

    // Update is called once per frame
    void Update() //Player Input
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            animator.SetFloat("Speed", 0);
            StartCoroutine(AttackCo());
            if (currentPlayer == CurrentPlayer.Rolan)
            {
                GetComponent<Projectile>().Fire(prevDirection);
            }
        }
        else if (currentState == PlayerState.walk)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);

                prevDirection = movement;
            }
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate() //Movement
    {
        if (currentState == PlayerState.walk)
        {
            movement.Normalize();
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

    }

    public void Knockback(float knockTime, float damage)
    {
        currentHealth.runtimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCoroutine(rb, knockTime));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FallInHole(AudioSource fallSound)
    {
        currentState = PlayerState.stagger;
        StartCoroutine(FallCo(fallSound));
    }

    public void PlayStep1()
    {
        if(!step1.isPlaying)
            step1.Play();
    }

    public void PlayStep2()
    {
        if(!step2.isPlaying)
            step2.Play();
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        
        switch(currentPlayer)
        {
            case CurrentPlayer.Kanith:
                yield return new WaitForSeconds(0.5f);
                break;
            case CurrentPlayer.Aramele:
                yield return new WaitForSeconds(0.4f);
                break;
            case CurrentPlayer.Percival:
                yield return new WaitForSeconds(0.7f);
                break;
            case CurrentPlayer.Rolan:
                yield return new WaitForSeconds(0.7f);
                break;
        }
        currentState = PlayerState.walk;
    }

    private IEnumerator KnockCoroutine(Rigidbody2D playerRB, float knockTime)
    {
        if (playerRB != null)
        {
            yield return new WaitForSeconds(1f * knockTime);

            playerRB.velocity = Vector2.zero;
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator FallCo(AudioSource fallSound)
    {
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(46, 18, 1);
        if(fallSound != null)
        {
            fallSound.Play();
        }
        animator.SetFloat("Vertical", 1);
        currentState = PlayerState.walk;
    }
}
