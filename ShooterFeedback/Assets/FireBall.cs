using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab; // The ball prefab to shoot
    public Transform firePoint;   // The position where the ball spawns
    public float ballSpeed = 10f; // Speed of the fired ball

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBall();
        }
    }

    void FireBall()
    {
        if (ballPrefab != null && firePoint != null)
        {
            // Instantiate the ball clone
            GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the ball clone
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * ballSpeed;
            }

            // Destroy the ball clone after 1 second
            Destroy(ball, 1f);
        }
        else
        {
            Debug.LogWarning("BallPrefab or FirePoint is not assigned.");
        }
    }
}
