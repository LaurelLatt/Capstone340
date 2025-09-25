using UnityEngine;

public class MushroomBounce : MonoBehaviour
{
    public float bounceForce = 50f; // tweak in inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.rigidbody;
        if (rb != null)
        {
            // Get the direction from the object to the player
            Vector2 direction = (collision.transform.position - transform.position).normalized;

            // Apply force away from the hitbox
            rb.linearVelocity = Vector2.zero; // reset velocity to make bounce clean
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector2 direction = (other.transform.position - transform.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }
    
    
}
