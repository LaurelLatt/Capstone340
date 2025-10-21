using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool isFlickering = false;
    private Collider2D platformCollider;
    private SpriteRenderer platformRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (isFlickering)
        {
            FlickerPlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisablePlatform()
    {
        platformRenderer.enabled = false;
    }

    private void FlickerPlatform()
    {
        StartCoroutine(TempDisablePlatform());
    }
    
    private IEnumerator TempDisablePlatform()
    {
        platformCollider.enabled = false;
        platformRenderer.color = new Color(1f, 0.5f, 0.5f, .7f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TempEnablePlatform());
    }

    private IEnumerator TempEnablePlatform()
    {
        platformCollider.enabled = true;
        platformRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TempDisablePlatform());
    }
}
