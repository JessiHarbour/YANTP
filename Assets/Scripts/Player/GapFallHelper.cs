using UnityEngine;

public class GapFallHelper : MonoBehaviour
{
    public LayerMask groundLayer;
    public float checkDistance = 0.2f;
    public float extraGravity = 25f;

    public Transform leftFoot;
    public Transform rightFoot;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        bool leftGround = Physics2D.Raycast(leftFoot.position, Vector2.down, checkDistance, groundLayer);
        bool rightGround = Physics2D.Raycast(rightFoot.position, Vector2.down, checkDistance, groundLayer);

        
        if (!leftGround && !rightGround)
        {
            rb.linearVelocity += Vector2.down * extraGravity * Time.fixedDeltaTime;
        }
    }
}