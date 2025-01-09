using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    [Header("On Death")]
    [SerializeField] GameObject pointPickUp;
    [SerializeField] int numberOfPoints = 3;
    [SerializeField] AudioClip damageSfx;

    protected Rigidbody2D rb;
    protected Collider2D col;
    protected Animator anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
 
    protected virtual void Die()
    {
        StopAllCoroutines();
      //  rb.Enemy1 = false;
        col.enabled = false;
        anim.SetTrigger("dead");
        AudioSource.PlayClipAtPoint(damageSfx, transform.position);

        for (int i = 0; i < numberOfPoints; i++)
        {
            Instantiate(pointPickUp, transform.position, Quaternion.identity);
        }
    }
}

