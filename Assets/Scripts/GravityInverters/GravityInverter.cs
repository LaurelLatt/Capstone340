using Movement;
using UnityEngine;

namespace GravityInverters
{
    public class GravityInverter : MonoBehaviour
    {
        [Tooltip("If true, flips gravity on enter. If false, flips on exit.")]
        public bool flipOnEnter = true;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (flipOnEnter && col.CompareTag("Player"))
            {
                Flip(col);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!flipOnEnter && col.CompareTag("Player"))
            {
                Flip(col);
            }
        }

        private void Flip(Collider2D col)
        {
            var movement = col.GetComponent<PlayerMovement>();
            if (movement != null)
            {
                movement.FlipGravity();
            }
        }
    }
}