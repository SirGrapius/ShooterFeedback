using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] bool isMoving = false;
    [SerializeField] bool isJumping = false;
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 10;

    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        rb.gravityScale = 2.1f;
    }


    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            rb.transform.position += Vector3.left * speed * Time.deltaTime;
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.transform.position += Vector3.right * speed * Time.deltaTime;
            isMoving = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
