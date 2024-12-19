using System.Drawing;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
  // need playh
    
    Rigidbody2D rb;
    [SerializeField] int enemylifes;
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float ledgeChecklenght = 1f;
    [SerializeField] AudioClip Walking;
    

    public int damageAmount = 10;

    private bool isFacingRight = true;
    private Collider2D other;

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
        Movement playerHealth = collision.gameObject.GetComponent<Movement>();
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
        other = this.GetComponent<Collider2D>();

        Debug.DrawRay(ledgeCheckPosition.position, Vector2.down * ledgeChecklenght, UnityEngine.Color.red);

    }
}   