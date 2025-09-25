using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
    
}
