using System.Collections;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color damageColor = Color.red;
    public float feedbackDuration = 0.2f;
    public int blinkCount = 3;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       originalColor = spriteRenderer.color;
    }

    public void ShowDamage()
    {
        StartCoroutine(FlashDamage());
    }

    private IEnumerator FlashDamage()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(feedbackDuration / (2 * blinkCount));
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(feedbackDuration / (2 * blinkCount));
        }
    }
}