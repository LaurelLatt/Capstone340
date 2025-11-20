using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformController : MonoBehaviour
{
    public bool isFlickering = false;
    private Tilemap tilemap;
    private TilemapCollider2D tilemapCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
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
        tilemapCollider.enabled = false;
    }

    private void FlickerPlatform()
    {
        StartCoroutine(FlickerThenDisable());
    }
    
    private IEnumerator FlickerThenDisable()
    {
        // Quick warning flicker (e.g., 3 times)
        for (int i = 0; i < 2; i++)
        {
            ChangePlatformOpacity(0.5f);   // dim
            yield return new WaitForSeconds(0.1f);

            ChangePlatformOpacity(1f);     // normal
            yield return new WaitForSeconds(0.1f);
        }

        // Now run your existing disable coroutine
        StartCoroutine(TempDisablePlatform());
    }
    
    private IEnumerator TempDisablePlatform()
    {
        tilemapCollider.enabled = false;
        ChangePlatformOpacity(.5f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(TempEnablePlatform());
    }

    

    private IEnumerator TempEnablePlatform()
    {
        tilemapCollider.enabled = true;
        ChangePlatformOpacity(1f);
        yield return new WaitForSeconds(2f);
        StartCoroutine(FlickerThenDisable());
    }
    
    private void ChangePlatformOpacity(float opacity)
    {
        Color c = tilemap.color;
        c.a = opacity;
        tilemap.color = c;
    }
}
