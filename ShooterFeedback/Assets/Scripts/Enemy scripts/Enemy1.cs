using System.Drawing;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int enemylifes;
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float ledgeChecklenght = 1f;

    public int damageAmount = 10;

    private bool isFacingRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private float GetLedgeChecklenght()
    {
        return ledgeChecklenght;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
    
        private void FixedUpdate()
    {
        rb.linearVelocityX = transform.right.x * enemyMovementSpeed;

        RaycastHit2D hit = Physics2D.Raycast(ledgeCheckPosition.position, Vector2.down, ledgeChecklenght, groundLayer);

        if (hit.collider == null) 
        {
            isFacingRight = !isFacingRight;

            if (isFacingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
        }
     
        Debug.DrawRay(ledgeCheckPosition.position, Vector2.down * ledgeChecklenght, UnityEngine.Color.red);


    }
}   