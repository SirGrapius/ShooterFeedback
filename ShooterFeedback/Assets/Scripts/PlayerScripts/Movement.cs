using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public bool isMoving;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isKnockback;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float groundCheckerRadius;
    [SerializeField] float knockbackForce = 10;
    [SerializeField] float knockbackDuration = 5;

    [SerializeField] public int health = 100;

    Vector2 playerInput;

    [Header ("Audio")]
    [SerializeField] AudioController ac;
    [SerializeField] AudioSource walkSource;
    [Header("GroundCheck")]
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundedLayers;
    [Header("Anim Settings")]
    [SerializeField] Animator myAnim;
    [SerializeField] Animator smokeAnim;
   
    [SerializeField] Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!isMoving)
        {
            if (!walkSource.isPlaying)
            {
                walkSource.Play();
            }
            else
            {
                walkSource.Stop();
            }
            if (isGrounded)
            {
                myAnim.Play("PlayerIdle");
            }
            StartCoroutine(endPuffCoroutine(0.2f));
            
        }
        else if (isGrounded)
        {
            myAnim.Play("PlayerWalk");
            smokeAnim.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            smokeAnim.Play("SmokePuff");
        }


        if (isGrounded)
        {
            if (!ac.sfxSource.isPlaying)
            {
                ac.playSFX(ac.audios[1]);
            }
            else
            {
                ac.sfxSource.Stop();
            }
        }
        else
        {
            if (!isMoving)
            {
                myAnim.Play("JumpAnim", 0, 0f);
            }
            if (isMoving)

            {
                StartCoroutine(endPuffCoroutine(0.2f));
                myAnim.Play("PlayerGlide");
            }
        }

        if (playerInput.x != 0)
        {
            isMoving = true;
            if (playerInput.x < 0)
            {
                rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (playerInput.x > 0)
            {
                rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (playerInput.x == 0)
        {
            isMoving = false;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            
            rb.linearVelocity = new Vector2(0, 0);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        isGrounded = Physics2D.OverlapCircle(groundChecker.position, groundCheckerRadius, groundedLayers);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
    }

    private void FixedUpdate()
    {
        if (!isKnockback)
        {
            rb.linearVelocityX = playerInput.x * speed;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillBrick")
        {
            TakeDamage(999999, null);
        }
    }

    public void TakeDamage(int damage, Collider2D other)
    {
        health -= damage;
        Vector2 direction = -(other.transform.position - this.transform.position).normalized;
        StartCoroutine(KnockbackCoroutine(direction));

        if (health <= 0)
        {
            WinLoseScript loader = FindAnyObjectByType<WinLoseScript>();

            loader.ScreenLoadProcess(1);
        }
    }
    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        isKnockback = true;
        Vector2 force = direction * knockbackForce;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }

    private IEnumerator endPuffCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        smokeAnim.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
