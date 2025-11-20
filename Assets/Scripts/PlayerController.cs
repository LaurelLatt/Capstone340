using GravityInverters;
using Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private HeightBasedGravityInverter gravityInverter;
    private PlayerMovement playerMovement;

    public bool isFrozen { get; private set; } = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        gravityInverter = GetComponent<HeightBasedGravityInverter>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFalling();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    private void CheckForFalling()
    {
        if (transform.position.y < -10 || transform.position.y > 20)
        {
            ResetPosition();
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LevelReset();
            }
        }
    }

    public void EnableHeightInversion()
    {
        gravityInverter.EnableGravityInverter();
    }

    public void DisableHeightInversion()
    {
        if (gravityInverter != null)
            gravityInverter.DisableGravityInverter();
        else
            DebugLogger.Log(LogChannel.Persistence,"Tried to disable height inversion, but no inverter found!", LogLevel.Warning);
    }

    public void Freeze()
    {
        // Freeze both X and Y position, but still prevent rotation
        rb.constraints = RigidbodyConstraints2D.FreezePositionX 
                         | RigidbodyConstraints2D.FreezeRotation;
        isFrozen = true;
    }

    public void Unfreeze()
    {
        // Allow movement, but keep rotation locked so the player doesn’t tilt
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isFrozen = false;
    }

    public void ResetMovement()
    {
        gravityInverter.IgnoreNextFrame();
        playerMovement.ResetMovement();
        gravityInverter.ResetState(false);
    }
    
    
    
}
