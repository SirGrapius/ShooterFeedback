using UnityEngine;

public class FireBall : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab; // The ball prefab to shoot
    public Transform firePoint;   // The position where the ball spawns
    public float ballSpeed = 10f; // Speed of the fired ball

    void Update()
    {
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (ballPrefab != null && firePoint != null)
        {
            // Instantiate the ball at the fire point's position and rotation
            GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);

            // Add velocity to the ball
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = firePoint.right * ballSpeed; // Fire the ball to the right
            }
        }
        else
        {
            Debug.LogWarning("BallPrefab or FirePoint is not assigned.");
        }
    }
}