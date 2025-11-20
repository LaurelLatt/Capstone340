using System.Collections;
using Audio;
using ScreenControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController player;
    public GameScreenController gameScreen;
    public LevelSettings[] allLevels;
    public CameraFade cameraFade;

    public LevelManager LevelManager { get; private set; }
    
    private int currentLevelIndex = 0;
    private AsyncOperation loadOperation;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelAsync(allLevels[index]));
        currentLevelIndex = index;
    }
    
    private IEnumerator LoadLevelAsync(LevelSettings settings)
    {
        
        // Load new level scene additively
        loadOperation = SceneManager.LoadSceneAsync(settings.levelName, LoadSceneMode.Additive);
        yield return loadOperation;
        
        // Switch to level music
        AudioManager.Instance.PlayLevelMusic();

        // Apply settings once the scene is loaded
        Scene levelScene = SceneManager.GetSceneByName(settings.levelName);
        SceneManager.SetActiveScene(levelScene);

        LevelManager = FindFirstObjectByType<LevelManager>();
        if (LevelManager != null)
        {
            LevelManager.ApplySettings(settings);
        }
        StartGame();
    }
    
    public void UnloadLevelAsync()
    {
        // Unload the current level if one is loaded
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }
        Time.timeScale = 0;

        AudioManager.Instance.PlayMenuMusic();
    }

    public void LevelReset()
    {
        if (LevelManager != null)
        {
            LevelManager.LevelReset();
        }
        else
        {
            DebugLogger.Log(LogChannel.Persistence, "LevelManager not found during LevelReset()", LogLevel.Warning);
        }
    }

    public void OnLevelComplete()
    {
        SaveSystem.UnlockLevel(currentLevelIndex + 1);
        StopGame();
        gameScreen.GoToResults();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }
}