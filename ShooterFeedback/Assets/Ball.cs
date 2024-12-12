using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameObject fireExplosionPrefab; // Explosion effect prefab
    public float lifetime = 1f;            // Time before clones self-destruct if no collision occurs
    public bool isMainBall = false;        // Flag to identify the main ball

    void Start()
    {
        // Destroy only if it's not the main ball
        if (!isMainBall)
        {
            Destroy(gameObject, lifetime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only destroy if it's not the main ball
        if (!isMainBall)
        {
            // Spawn fire explosion at the collision point
            if (fireExplosionPrefab != null)
            {
                Instantiate(fireExplosionPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the ball (clone)
            Destroy(gameObject);
        }
    }
}
