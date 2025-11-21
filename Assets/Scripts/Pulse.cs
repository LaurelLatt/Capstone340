using UnityEngine;

public class Pulse : MonoBehaviour
{
    public float speed = 2f;     // how fast it pulses
    public float intensity = 0.2f; // how big the pulse is
    
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) * intensity) + 1f;
        transform.localScale = startScale * t;
    }
}
