using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private bool moveOnSpawn = false;
    [SerializeField] private AudioClip pickUpSfx;

    private Collider2D col;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        
        if (rb == null) Debug.LogError("Rigidbody2D is missing!");
        if (col == null) Debug.LogError("Collider2D is missing!");

    
        if (moveOnSpawn)
        {
            rb.AddForce(new Vector2(Random.Range(-6, 6), Random.Range(2, 4) * 3), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-2, 2) * 5, ForceMode2D.Impulse);
        }

       
        col.enabled = false;
        StartCoroutine(EnableColliderAfterDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");

       
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player picked up the point!");

          
            if (pickUpSfx != null)
            {
                AudioSource.PlayClipAtPoint(pickUpSfx, transform.position);
            }
            else
            {
                Debug.LogWarning("PickUpSfx is not assigned!");
            }

            
            Destroy(gameObject);
        }
    }

    private IEnumerator EnableColliderAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
        Debug.Log("Collider enabled.");
    }
}