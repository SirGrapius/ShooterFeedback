using UnityEngine;

public class Magicspell : MonoBehaviour
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