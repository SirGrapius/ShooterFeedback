using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int enemylifes;
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float ledgeChecklenght;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private float GetLedgeChecklenght()
    {
        return ledgeChecklenght;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = transform.right.x * enemyMovementSpeed;

        RaycastHit2D hit = Physics2D.Raycast(ledgeCheckPosition.position, Vector2.down, ledgeChecklenght, groundLayer);

        if (hit.collider != null) 
        { 
        
        }
        else
        {
        
        }

    //    Debug.DrawRay(ledgeCheckPosition.position, Vector2.down, ledgeChecklenght, Color.red);
    }
}   