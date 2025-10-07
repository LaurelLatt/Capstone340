using Movement;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite movingSprite;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private PlayerMovement player;
    

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
        player = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Check horizontal movement
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            sr.sprite = movingSprite;

            // Flip sprite depending on direction
            FlipMovingSprite();
        }
        else
        {
            sr.sprite = standingSprite;
        }
    }

    private void FlipMovingSprite()
    {
        // right side up flipping
        if (player.gravityDirection == 1)
        {
            if (rb.linearVelocity.x > 0)
                sr.flipX = true;
            else if (rb.linearVelocity.x < 0)
                sr.flipX = false;
        }
        // upside down flipping
        else if (player.gravityDirection == -1)
        {
            if (rb.linearVelocity.x < 0)
                sr.flipX = true;
            else if (rb.linearVelocity.x > 0)
                sr.flipX = false;
        }
    }
}
