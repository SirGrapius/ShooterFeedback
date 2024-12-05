using UnityEditorInternal;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField] bool isGrounded;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float groundCheckerRadius;

    Vector2 playerInput;

    [SerializeField] AudioController ac;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundedLayers;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {

        playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //TEST!!!
        if (isMoving)
        {
            if (!ac.sfxSource.isPlaying)
            {
                ac.playSFX(ac.audios[0]);
            }
            else
            {
                ac.sfxSource.Stop();
            }
        }
        //TEST!!!
        if (!isGrounded)
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


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(0,0);
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
        rb.linearVelocityX = playerInput.x * speed;
    }
}
