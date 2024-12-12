using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died.");
        Destroy(gameObject);
     
    }
}

