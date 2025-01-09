using UnityEngine;

public class SquashandStretch : MonoBehaviour
{
    public Transform Sprite;
    public float Stretch = 0.1f;
    [SerializeField] private Transform squashParent;

    private Rigidbody2D rb;
    private Vector3 originalScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = Sprite.transform.localScale;

        if (!squashParent)
            squashParent = new GameObject(string.Format("_squash_{0}", name)).transform;
    }

    private void Update()
    {
        Sprite.parent = transform;
        Sprite.localPosition = Vector3.zero;
        Sprite.localScale = originalScale;
        Sprite.localRotation = Quaternion.identity;

        squashParent.localScale = Vector3.one;
        squashParent.position = transform.position;

        Vector3 velocity = rb.linearVelocity;
        if (velocity.sqrMagnitude > 0.01f)
        {
            squashParent.rotation = Quaternion.FromToRotation(Vector3.right, velocity);
        }

        var scaleX = 1.0f + (velocity.magnitude * Stretch);
        var scaleY = 1.0f / scaleX;
        Sprite.parent = squashParent;
        squashParent.localScale = new Vector3(scaleX, scaleY, 1.0f);
    }
}
