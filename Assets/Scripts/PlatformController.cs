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
        StartCoroutine(TempDisablePlatform());
    }
    
    private void ChangePlatformOpacity(float opacity)
    {
        Color c = tilemap.color;
        c.a = opacity;
        tilemap.color = c;
    }
}
