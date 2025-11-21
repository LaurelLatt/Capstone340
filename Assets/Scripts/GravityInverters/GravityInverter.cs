using Movement;
using UnityEngine;

namespace GravityInverters
{
    public class GravityInverter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                Flip(col);
                gameObject.SetActive(false);
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