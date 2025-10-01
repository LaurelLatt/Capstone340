using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
    public Sprite standingSprite;
    public Sprite movingSprite;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        // Check horizontal movement
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
            sr.sprite = movingSprite;

            // Flip sprite depending on direction
            if (rb.linearVelocity.x > 0)
                sr.flipX = true;
            else if (rb.linearVelocity.x < 0)
                sr.flipX = false;
        }
        else
        {
            sr.sprite = standingSprite;
        }
    }
}
