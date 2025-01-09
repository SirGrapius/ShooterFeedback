using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;

public class DecorationScript : MonoBehaviour
{
    [SerializeField] int health = 4;
    [SerializeField] bool pCollision = false;
    [SerializeField] UnityEngine.Color baseColor;
    [SerializeField] UnityEvent myEvent;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator myAnim;
    [SerializeField] Movement player;
    void Start()
    {
        myAnim = spriteRenderer.gameObject.GetComponent<Animator>();
        baseColor = spriteRenderer.color;
        myAnim.Play("BushIdle");
    }


    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 1;
            StartCoroutine(DamageIndicator());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && myEvent != null)
        {
            myEvent.Invoke();
        }
        else
        {
            print("No event to invoke :(");
        }
    }

    IEnumerator DamageIndicator()
    {
        spriteRenderer.color = new UnityEngine.Color(1f, 0.6635219f, 0.6776597f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = baseColor;
    }
}
