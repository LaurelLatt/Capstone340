using GravityInverters;
using Movement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private HeightBasedGravityInverter gravityInverter;
    private PlayerMovement playerMovement;
    
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
        if (transform.position.y < -30)
        {
            ResetPosition();
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
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
    }

    public void Unfreeze()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    public void ResetMovement()
    {
        gravityInverter.IgnoreNextFrame();
        playerMovement.ResetMovement();
        gravityInverter.ResetState(false);
    }
    
    
    
}
