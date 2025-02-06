using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class BulletScript : MonoBehaviour
{
    [SerializeField] UnityEvent myEvent;

    [SerializeField] Animator myAnim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] ScreenShake myCamera;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myAnim.Play("FirePulse", 0, 0f);
        myCamera = FindAnyObjectByType<ScreenShake>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy1 enemyScript = collision.gameObject.GetComponent<Enemy1>();
            enemyScript.TakeDamage(1);
            StartCoroutine(Explode());
        }
        else
        {
            StartCoroutine(Explode());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Decoration")
        {
            DecorationScript decScript = collision.gameObject.GetComponent<DecorationScript>();
            decScript.TakeDamage();
            myEvent.Invoke();
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        rb.linearVelocity = new Vector2(0,0);
        myEvent.Invoke();
        StartCoroutine(myCamera.Shake(1, 1));

        yield return new WaitForSeconds(0.583f);

        Destroy(gameObject);
        yield return null;
    }
}
