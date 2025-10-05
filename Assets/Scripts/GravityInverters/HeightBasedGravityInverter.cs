using Movement;
using UnityEngine;

namespace GravityInverters
{
    public class HeightBasedGravityInverter : MonoBehaviour
    {
        [Tooltip("The Y position at which gravity flips.")]
        public float flipHeight = 0f;

        private bool isInverted = false;
        private PlayerMovement movement;
        private float lastY;

        private void Start()
        {
            movement = GetComponent<PlayerMovement>();
            lastY = transform.position.y;
        }

        private void FixedUpdate()
        {
            if (movement == null) return;

            if (transform.position.y > flipHeight && !isInverted)
            {
                movement.FlipGravity();
                isInverted = true;
            }
            else if (transform.position.y <= flipHeight && isInverted)
            {
                movement.FlipGravity();
                isInverted = false;
            }

        }
    }
}
