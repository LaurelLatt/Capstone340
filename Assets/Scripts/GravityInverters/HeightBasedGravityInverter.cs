using Movement;
using UnityEngine;

namespace GravityInverters
{
    public class HeightBasedGravityInverter : MonoBehaviour
    {
        [Tooltip("The Y position at which gravity flips.")]
        public float flipHeight = 0f;
        
        [SerializeField] private float flipBuffer = 0.5f; // prevents repeated flipping

        private bool isInverted = false;
        private PlayerMovement movement;
        private bool canInvert = false;

        private void Start()
        {
            movement = GetComponent<PlayerMovement>();
            
        }

        private void FixedUpdate()
        {
            if (!canInvert) return;
            if (movement == null) return;

            if (transform.position.y > flipHeight + flipBuffer && !isInverted)
            {
                movement.FlipGravity();
                isInverted = true;
            }
            else if (transform.position.y <= flipHeight - flipBuffer && isInverted)
            {
                movement.FlipGravity();
                isInverted = false;
            }

        }

        public void EnableGravityInverter()
        {
            canInvert = true;
        }

        public void DisableGravityInverter()
        {
            canInvert = false;
        }
    }
}
