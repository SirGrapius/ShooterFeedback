using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public class Enemy1 : MonoBehaviour
{
  
    
    Rigidbody2D rb;
    
    [SerializeField] float enemyMovementSpeed;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] bool isGrounded;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float ledgeChecklenght = 1f;
    [SerializeField] AudioClip Walking;
    [SerializeField] int EnemyHealth = 50;

    [SerializeField] UnityEvent myEvent;
    [SerializeField] GameObject deadBody;
    private int currentHealth;


    public int damageAmount = 10;

    private bool isFacingRight = true;
    private Collider2D other;
    private Animator anim;


    bool enemyIsDead;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = EnemyHealth;
        anim.Play("Enemy_slide");
    }

    private float GetLedgeChecklenght()
    {
        return ledgeChecklenght;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            if(!enemyIsDead)
            {
                Die();
                enemyIsDead = true;
            }
          
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        StartCoroutine(Death());
    }




private void OnCollisionEnter2D(Collision2D collision)
   {
        if (collision.gameObject.CompareTag("Player"))
        {
        Movement playerHealth = collision.gameObject.GetComponent<Movement>();
         if (playerHealth != null)
            {
              playerHealth.TakeDamage(damageAmount, other = this.GetComponent<Collider2D>());

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

    IEnumerator Death()
    {
        //myEvent.Invoke();
        anim.SetTrigger("IsDead");
        yield return new WaitForSeconds(0.75f);
        Instantiate(deadBody, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
    }
}   