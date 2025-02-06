using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float riseRate;
    [SerializeField] bool rising = true;
    [SerializeField] float startDist;
    [SerializeField] float flatForce;

    [SerializeField] Vector3 startPos;
    [SerializeField] Rigidbody2D rb;
    void Start()
    {
        startPos = transform.localPosition;
        rb = this.GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        startDist = (startPos.y - transform.localPosition.y);

        

        if (rising && startDist > 0)
        {
            Debug.Log("rising");
            rb.AddForceY(startDist * Time.deltaTime * riseRate + flatForce);
        }
        if (rising && startDist < 0)
        {
            rb.AddForceY(-startDist * Time.deltaTime * riseRate - flatForce);
        }

        if (startDist <= 0)
        {
            Debug.Log("working");
            rb.linearVelocityY = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rising = true;
        }
    }
}
