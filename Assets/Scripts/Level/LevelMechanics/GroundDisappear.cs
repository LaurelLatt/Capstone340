using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundDisappear : MonoBehaviour
{
    public Tilemap groundTilemap; // assign in Inspector
    public Vector3Int regionStart; // top-left (or chosen) tile of the area to open
    public Vector2Int regionSize = new Vector2Int(3, 1); // width x height
    public float duration = 3f; // how long before it comes back

    private bool triggered = false;
    private List<(Vector3Int pos, TileBase tile)> storedTiles = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        StartCoroutine(DisappearAndReappear());
    }

    private IEnumerator DisappearAndReappear()
    {
        triggered = true;

        // Store tiles in region
        storedTiles.Clear();
        for (int x = 0; x < regionSize.x; x++)
        {
            for (int y = 0; y < regionSize.y; y++)
            {
                Vector3Int pos = regionStart + new Vector3Int(x, -y, 0);
                TileBase tile = groundTilemap.GetTile(pos);
                if (tile != null)
                {
                    storedTiles.Add((pos, tile));
                    groundTilemap.SetTile(pos, null); // remove
                }
            }
        }

        // Wait a few seconds
        yield return new WaitForSeconds(duration);

        // Restore tiles
        foreach (var (pos, tile) in storedTiles)
            groundTilemap.SetTile(pos, tile);

        triggered = false; // allow it to trigger again
    }
}

