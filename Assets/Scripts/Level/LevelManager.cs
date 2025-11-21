using System;
using System.Collections;
using GravityInverters;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public static event Action OnCollectibleCollected;
    public static event Action OnLevelReset;
    public int goalCollectibles {get; private set;}
    public int collectedItems {get; private set;}
    
    private Vector3 playerSpawn;
    private bool hasCollectibles;
    private LevelSettings levelSettings;
    private PlayerController player;

    public void ApplySettings(LevelSettings settings)
    {
        levelSettings = settings;
        // Set up the player’s starting position
        player = GameManager.Instance.player;
        if (player != null)
        {
            ResetSettings();
            
            Vector3 spawn = settings.spawnPosition;
            player.transform.position = spawn;
            playerSpawn = spawn;
            
            player.SetBounds(levelSettings.upperBound, levelSettings.lowerBound);

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
            goalCollectibles = settings.goalCollectibles;
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
            
            switch ((settings.fadeWaitTime > 0, settings.playerFreeze))
            {
                case (true, true):
                    GameManager.Instance.cameraFade.StartFadeLoopWithFreeze(settings.fadeWaitTime);
                    break;
                case (true, false):
                    GameManager.Instance.cameraFade.StartFadeLoop(settings.fadeWaitTime);
                    break;
                case (false, true):
                    GameManager.Instance.cameraFade.StartFadeLoopWithFreeze();
                    break;
                case (false, false):
                    GameManager.Instance.cameraFade.StartFadeLoop();
                    break;
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
        
        player.transform.position = playerSpawn;

        if (hasCollectibles)
        {
            GameManager.Instance.gameScreen.ShowCollectedText();
            ResetCollectibles();
        }
    }

    public void LevelReset()
    {
        OnLevelReset?.Invoke();
        ResetCollectibles();
        ResetGravityInverters();
        
        if (player != null)
        {
            player.RespawnAt(playerSpawn);
        }
    }

    private void ResetGravityInverters()
    {
        foreach (var inverter in FindObjectsByType<GravityInverter>(FindObjectsInactive.Include,
                     FindObjectsSortMode.None))
        {
            inverter.gameObject.SetActive(true);
        }
    }

    private void ResetCollectibles()
    {
        // Re-enable all collectibles in the level
        foreach (var collectible in FindObjectsByType<Collectible>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            collectible.gameObject.SetActive(true);
        }
        
        collectedItems = 0;
        GameManager.Instance.gameScreen.UpdateCollectibleUI(collectedItems);
    }

    public void CollectItem()
    {
        if (!hasCollectibles) return;

        collectedItems++;
        OnCollectibleCollected?.Invoke();
        GameManager.Instance.gameScreen.UpdateCollectibleUI(collectedItems);

        if (collectedItems >= goalCollectibles && levelSettings.collectiblesAsOnlyWinCondition)
            LevelComplete();
    }

    private bool CheckCollectibleGoalComplete()
    {
        if (collectedItems >= goalCollectibles)
        {
            return true;
        }
        return false;
    }
    

    public void LevelComplete()
    {
        if (levelSettings.collectiblesAsAWinCondition)
        {
            bool goalComplete = CheckCollectibleGoalComplete();
            if (goalComplete)
            {
                DebugLogger.Log(LogChannel.Gameplay, "Level Complete!");
                StartCoroutine(LevelCompleteRoutine());
            }
        }
        else {
            DebugLogger.Log(LogChannel.Gameplay, "Level Complete!");
            StartCoroutine(LevelCompleteRoutine());
        }
        
    }

    private IEnumerator LevelCompleteRoutine()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OnLevelComplete();
        
    }
    private void ResetSettings()
    {
        player.DisableHeightInversion();
        player.transform.position = Vector3.zero;
        player.ResetMovement();
        ResetCollectibles();
        ResetGravityInverters();
        hasCollectibles = false;
        GameManager.Instance.gameScreen.HideCollectedText();
        GameManager.Instance.cameraFade.TurnOffFade();
        
    }
}
