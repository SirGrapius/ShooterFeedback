using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb; 
    public float knockbackForce = 5f; 
    public float knockbackDuration = 0.2f;

    private bool isKnockedBack = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (!isKnockedBack)
        {
            StartCoroutine(KnockbackRoutine(direction));
        }
    }

    private IEnumerator KnockbackRoutine(Vector2 direction)
    {
        isKnockedBack = true;

        
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);

     
        yield return new WaitForSeconds(knockbackDuration);

       
        isKnockedBack = false;
    }
}