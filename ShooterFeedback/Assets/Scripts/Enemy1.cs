using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [SerializeField] Vector2 wallCheckPos;
    [SerializeField] Vector2 groundCheckPos;
    [SerializeField] LayerMask checkMask;

    [Header("Gizmos")]
    [SerializeField] Color checkPosColors = Color.red;
    [SerializeField] float checkPosSize = 0.1f;

    private bool isFacingRight = true;

    private void FixedUpdate()
    {
        if (CollisionCheck(wallCheckPos) || !CollisionCheck(groundCheckPos))
        {
            isFacingRight = !isFacingRight;
        }

        float horizontalVelocity = (isFacingRight) ? moveSpeed : -moveSpeed;
       // rb.linearVelocity = new Vector2(horizontalVelocity, rb.linearVelocity.y);
    }

    private bool CollisionCheck(Vector2 positionToCheck)
    {
        positionToCheck = (Vector2)transform.position + new Vector2((isFacingRight) ? positionToCheck.x : -positionToCheck.x, positionToCheck.y);

        if (Physics2D.OverlapCircle(positionToCheck, 0.1f, checkMask))
        {
            return true;
        }

        return false;
    }


    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = checkPosColors;

        Vector2 myPosition = (Vector2)transform.position;
        Vector2 wallCheckActualPos = myPosition + new Vector2((isFacingRight) ? wallCheckPos.x : -wallCheckPos.x, wallCheckPos.y);
        Vector2 groundCheckActualPos = myPosition + new Vector2((isFacingRight) ? groundCheckPos.x : -groundCheckPos.x, groundCheckPos.y);
        Gizmos.DrawWireSphere(wallCheckActualPos, checkPosSize);
        Gizmos.DrawWireSphere(groundCheckActualPos, checkPosSize);
    }
}
