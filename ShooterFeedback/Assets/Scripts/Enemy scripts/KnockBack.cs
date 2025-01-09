using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb; // Referens till spelarens Rigidbody2D
    public float knockbackForce = 5f; // Hur h�rt knockback ska vara
    public float knockbackDuration = 0.2f; // Hur l�nge spelaren �r knockad

    private bool isKnockedBack = false; // F�r att hindra att spelaren r�r sig normalt under knockback

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // H�mta Rigidbody2D-komponenten
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

        // St�ng av normal r�relse (om du har ett r�relseskript)
        rb.linearVelocity = Vector2.zero; // Stoppar nuvarande r�relse
        rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse); // L�gg till knockback

        // V�nta medan knockback �r aktiv
        yield return new WaitForSeconds(knockbackDuration);

        // Till�t normal r�relse igen
        isKnockedBack = false;
    }
}