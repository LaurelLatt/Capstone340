using UnityEngine;

// class received from Jon Humphreys
public class CameraFollow2D : MonoBehaviour
{
    [Tooltip("The transform the camera should follow.")]
    public Transform target;

    [Tooltip("How quickly the camera moves to follow the target. 0 = instant snap.")]
    [Range(0f, 10f)]
    public float smoothSpeed = 5f;

    [Tooltip("Y-offset so the player isn't centered on the screen. Positive for higher camera.")]
    public float yOffset = 2;
    
    [Tooltip("Z-offset so the camera stays back (usually negative).")]
    public float zOffset = -10f;
    
    [Tooltip("Top and bottom position of the camera.")]
    public float minY = -10f;
    public float maxY = 10f;
    
    private void LateUpdate()
    {
        if (target == null) return;
        
        float targetY = target.position.y + yOffset;
        
        // Clamp the Y position so camera doesn't go above maxY or below minY
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Desired camera position: follow target in X/Y, keep fixed Z
        Vector3 desired = new Vector3(target.position.x, targetY, zOffset);

        if (smoothSpeed <= 0f)
        {
            // Instant snap
            transform.position = desired;
        }
        else
        {
            // Smooth follow
            transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Change what the camera follows at runtime.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
}