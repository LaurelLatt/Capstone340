using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Vector3 playerSpawn;

    [Header("Collectibles")]
    [SerializeField] private bool hasCollectibles;
    [SerializeField] private int totalCollectibles;

    private int collectedItems;

    public void ApplySettings(LevelSettings settings)
    {
        // Set up the player’s starting position
        var player = GameManager.Instance.player;
        if (player != null)
        {
            Vector3 spawn = settings.spawnPosition;
            player.transform.position = spawn;
            playerSpawn = spawn;

            // Apply gravity inversion or other toggles
            Debug.Log($"Height inversion enabled? {settings.heightInversionEnabled}");
            if (settings.heightInversionEnabled)
            {
                player.EnableHeightInversion();
            }
            else
            {
                player.DisableHeightInversion();
            }
        }

        // Initialize collectible UI
        if (settings.hasCollectibles)
        {
            hasCollectibles = true;
            totalCollectibles = settings.totalCollectibles;
            ResetCollectibles();
            GameManager.Instance.gameScreen.ShowCollectedText();
            
        }
        else
        {
            hasCollectibles = false;
            GameManager.Instance.gameScreen.HideCollectedText();
        }
    }

    private void Start()
    {
        // Fallback setup if no ScriptableObject settings were provided
        if (GameManager.Instance == null) return;

        var player = GameManager.Instance.player;
        
        player.transform.position = playerSpawn;

        if (hasCollectibles)
        {
            GameManager.Instance.gameScreen.ShowCollectedText();
            ResetCollectibles();
        }
    }

    public void LevelReset()
    {
        ResetCollectibles();
        // Re-enable all collectibles in the level
        foreach (var collectible in FindObjectsByType<Collectible>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            collectible.gameObject.SetActive(true);
        }
        GameManager.Instance.player.transform.position = playerSpawn;
    }

    private void ResetCollectibles()
    {
        collectedItems = 0;
        GameManager.Instance.gameScreen.UpdateCollectibleUI(collectedItems);
    }

    public void CollectItem()
    {
        if (!hasCollectibles) return;

        collectedItems++;
        GameManager.Instance.gameScreen.UpdateCollectibleUI(collectedItems);

        if (collectedItems >= totalCollectibles)
            LevelComplete();
    }

    private void LevelComplete()
    {
        Debug.Log("Level Complete!");
        GameManager.Instance.OnLevelComplete();
    }
}
