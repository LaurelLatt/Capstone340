using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [Header("Scene References")]
    [SerializeField] private Vector3 playerSpawn;

    [Header("Collectibles")]
    [SerializeField] private bool hasCollectibles;
    [SerializeField] private int totalCollectibles;

    private LevelSettings levelSettings;
    private int collectedItems;

    public void ApplySettings(LevelSettings settings)
    {
        levelSettings = settings;
        // Set up the player’s starting position
        PlayerController player = GameManager.Instance.player;
        if (player != null)
        {
            ResetSettings(player);
            
            Vector3 spawn = settings.spawnPosition;
            player.transform.position = spawn;
            playerSpawn = spawn;

            // Apply gravity inversion or other toggles
            DebugLogger.Log(LogChannel.Gameplay, $"Height inversion enabled? {settings.heightInversionEnabled}");
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

        ApplyCameraFadeSettings(settings);
    }

    private static void ApplyCameraFadeSettings(LevelSettings settings)
    {
        DebugLogger.Log(LogChannel.Gameplay,$"Camera fade enabled?: {settings.hasCameraFade}");
        if (settings.hasCameraFade)
        {
            GameManager.Instance.cameraFade.TurnOnFade();
            if (settings.fadeWaitTime > 0)
            {
                GameManager.Instance.cameraFade.StartFadeLoop(settings.fadeWaitTime);
            }
            else
            {
                GameManager.Instance.cameraFade.StartFadeLoop();
            }
        }
        else
        {
            GameManager.Instance.cameraFade.TurnOffFade();
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

        if (collectedItems >= totalCollectibles && levelSettings.collectiblesAsWinCondition)
            LevelComplete();
    }

    private void LevelComplete()
    {
        DebugLogger.Log(LogChannel.Gameplay, "Level Complete!");
        GameManager.Instance.OnLevelComplete();
    }

    private void ResetSettings(PlayerController player)
    {
        player.DisableHeightInversion();
        player.transform.position = Vector3.zero;
        player.ResetMovement();
        ResetCollectibles();
        hasCollectibles = false;
        GameManager.Instance.gameScreen.HideCollectedText();
        GameManager.Instance.cameraFade.TurnOffFade();
        
    }
}
