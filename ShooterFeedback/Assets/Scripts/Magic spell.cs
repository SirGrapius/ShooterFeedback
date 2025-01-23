using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
     
        if (collision.CompareTag("Enemy"))
        {
        
            Destroy(collision.gameObject); 
        }

       
        Destroy(gameObject);
    }
}