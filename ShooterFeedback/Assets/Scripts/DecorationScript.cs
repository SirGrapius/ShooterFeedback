using System.Collections;
using System.Drawing;
using UnityEngine;

public class DecorationScript : MonoBehaviour
{
    [SerializeField] int health = 4;
    [SerializeField] UnityEngine.Color baseColor;

    [SerializeField] SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;
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

    IEnumerator DamageIndicator()
    {
        spriteRenderer.color = new UnityEngine.Color(1f, 0.6635219f, 0.6776597f);
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = baseColor;
    }
}
