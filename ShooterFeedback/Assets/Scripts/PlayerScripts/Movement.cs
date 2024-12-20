using System.Collections;
using UnityEditorInternal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isKnockback;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float groundCheckerRadius;
    [SerializeField] public int health = 100;
    [SerializeField] float knockbackForce = 10;
    [SerializeField] float knockbackDuration = 5;

    Vector2 playerInput;

    [SerializeField] AudioController ac;
    [SerializeField] AudioSource walkSource;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundedLayers;
    [SerializeField] Animator myAnim;
    [SerializeField] Rigidbody2D rigidbody;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        }
        else if (isGrounded)
        {
            myAnim.Play("PlayerWalk");
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
        else if (isMoving)
        {
            myAnim.Play("PlayerJump");
        }

        if (playerInput.x != 0)
        {
            isMoving = true;
            if (playerInput.x < 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (playerInput.x > 0)
            {
                rigidbody.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (playerInput.x == 0)
        {
            isMoving = false;
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.linearVelocity = new Vector2(0, 0);
            rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
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
            rigidbody.linearVelocityX = playerInput.x * speed;

        }
    }

    public void TakeDamage(int damage, Collider2D other)
    {
        health -= damage;
        Vector2 direction = -(other.transform.position - this.transform.position).normalized;
        StartCoroutine(KnockbackCoroutine(direction));
    }
    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        isKnockback = true;
        Vector2 force = direction * knockbackForce;
        rigidbody.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }
}
