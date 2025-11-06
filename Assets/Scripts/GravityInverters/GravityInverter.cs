using Movement;
using UnityEngine;

namespace GravityInverters
{
    public class GravityInverter : MonoBehaviour
    {
        [Tooltip("If true, flips gravity on enter. If false, flips on exit.")]
        public bool flipOnEnter = true;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (flipOnEnter && col.collider.CompareTag("Player"))
            {
                Flip(col.collider);
            }
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            if (col.collider.CompareTag("Player"))
            {
                Flip(col.collider);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Flip(col);
            }
        }

        private void Flip(Collider2D col)
        {
            var movement = col.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                DebugLogger.Log(LogChannel.Gameplay, "Flipping gravity");
                movement.FlipGravity();
            }
        }
    }
}