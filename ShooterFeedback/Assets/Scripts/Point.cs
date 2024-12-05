using System.Collections;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] bool moveOnSpawn = false;
    [SerializeField] AudioClip pickUpSfx;

    private Collider2D col;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (moveOnSpawn)
        {
            rb.AddForce(new Vector3(Random.Range(-6, 6), Random.Range(2, 4) * 3), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-2, 2) * 5, ForceMode2D.Impulse);
        }

        col.enabled = false;
        StartCoroutine(DelayTrigger());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(pickUpSfx, transform.position);
            Destroy(gameObject);
        }
    }

    IEnumerator DelayTrigger()
    {

        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
    }
}
