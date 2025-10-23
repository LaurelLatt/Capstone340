using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Transform playerSpawn;

    [Header("Collectibles")]
    [SerializeField] private bool hasCollectibles;
    [SerializeField] private int totalCollectibles;

    private int collected;

    public void ApplySettings(LevelSettings settings)
    {
        // Set up the player’s starting position
        var player = GameManager.Instance.player;
        if (player != null)
        {
            Vector3 spawn = settings.spawnPosition != Vector3.zero 
                ? settings.spawnPosition 
                : (playerSpawn != null ? playerSpawn.position : Vector3.zero);

            player.transform.position = spawn;

            // Apply gravity inversion or other toggles
            if (settings.heightInversionEnabled)
                player.EnableHeightInversion();
            else
                player.DisableHeightInversion();
        }

        // Initialize collectible UI
        if (settings.hasCollectibles)
        {
            hasCollectibles = true;
            totalCollectibles = settings.totalCollectibles;
            collected = 0;
            GameManager.Instance.GameScreen.ShowCollectedText();
            GameManager.Instance.GameScreen.UpdateCollectibleUI(collected);
        }
        else
        {
            hasCollectibles = false;
            GameManager.Instance.GameScreen.HideCollectedText();
        }
    }

    private void Start()
    {
        // Fallback setup if no ScriptableObject settings were provided
        if (GameManager.Instance == null) return;

        var player = GameManager.Instance.player;
        if (playerSpawn != null)
            player.transform.position = playerSpawn.position;

        if (hasCollectibles)
        {
            GameManager.Instance.GameScreen.ShowCollectedText();
            GameManager.Instance.GameScreen.UpdateCollectibleUI(0);
        }
    }

    public void LevelReset()
    {
        collected = 0;
        GameManager.Instance.GameScreen.UpdateCollectibleUI(collected);
        GameManager.Instance.player.transform.position = playerSpawn.position;
    }

    public void CollectItem()
    {
        if (!hasCollectibles) return;

        collected++;
        GameManager.Instance.GameScreen.UpdateCollectibleUI(collected);

        if (collected >= totalCollectibles)
            LevelComplete();
    }

    private void LevelComplete()
    {
        Debug.Log("Level Complete!");
        // Optionally trigger GameManager.Instance.OnLevelComplete();
    }
}
