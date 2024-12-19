using UnityEditorInternal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField] bool isGrounded;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float groundCheckerRadius;
    [SerializeField] public int health = 100;

    Vector2 playerInput;

    [SerializeField] AudioController ac;
    [SerializeField] AudioSource walkSource;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundedLayers;
    [SerializeField] Animator myAnim;
    [SerializeField] Rigidbody2D rigidbody;

    void Start()
    {
        myAnim = GetComponent<Animator>();
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
        }
        else
        {
            myAnim.Play("Walking");
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
            myAnim.Play("Jumping");
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
            rigidbody.linearVelocity = new Vector2(0,0);
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
        rigidbody.linearVelocityX = playerInput.x * speed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

}
