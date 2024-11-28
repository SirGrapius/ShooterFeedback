using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool isMoving;
    [SerializeField] bool isGrounded;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;
    [SerializeField] float groundCheckerRadius;

    Vector2 playerInput;

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

        rb.linearVelocityX = playerInput.x * speed;

        if(playerInput.x != 0)
        {
            isMoving = true;
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
        rb.linearVelocity = new Vector2(playerInput.x * speed, rb.linearVelocity.y);
    }
}
