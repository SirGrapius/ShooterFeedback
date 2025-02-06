using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] bool onCD;
    [SerializeField] float speed;
    [SerializeField] float cd = 0.5f;
    [SerializeField] float animTime = 0.5f;
    [SerializeField] UnityEvent myEvent;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform fingerPoint;
    [SerializeField] ScreenShake shaker;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onCD == false)
        {
            StartCoroutine(Cooldown());
            StartCoroutine(Shoot());   
        }
    }

    IEnumerator Cooldown()
    {
        onCD = true;

        yield return new WaitForSeconds(cd);

        onCD = false;

        yield return null;
    }

    IEnumerator Shoot()
    {
        FindAnyObjectByType<Movement>().isShooting = true;
        myEvent.Invoke();

        yield return new WaitForSeconds(animTime);

        GameObject FireBall = Instantiate(bullet, fingerPoint.position, fingerPoint.rotation);
        StartCoroutine(shaker.Shake(0.5f, 0.25f));

        Rigidbody2D rb = FireBall.GetComponent<Rigidbody2D>();

        rb.linearVelocity = fingerPoint.right * speed;
        FindAnyObjectByType<Movement>().isShooting = false;

        yield return null;
    }
}
