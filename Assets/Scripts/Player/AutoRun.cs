using UnityEngine;

public class AutoRun : MonoBehaviour
{
    public float speed = 3f;
    Rigidbody2D rb;

    // animation
    public Sprite[] walkSprites; 
    private SpriteRenderer sr;

    private int index = 0;
    private float timer = 0f;
    public float animationSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
       
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);

     
        float moveSpeed = Mathf.Abs(rb.linearVelocity.x);

        if (moveSpeed > 0.1f)
        {
            timer += Time.deltaTime;

            if (timer >= animationSpeed)
            {
                index = (index + 1) % walkSprites.Length;
                sr.sprite = walkSprites[index];
                timer = 0f;
            }
        }
    }
}