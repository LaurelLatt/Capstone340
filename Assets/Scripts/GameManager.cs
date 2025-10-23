using System.Collections;
using ScreenControllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PlayerController player;
    public GameScreenController GameScreen;
    public LevelSettings[] allLevels;

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
    }
    
    private IEnumerator LoadLevelAsync(LevelSettings settings)
    {
        // Unload previous level if one is loaded
        if (SceneManager.sceneCount > 1)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }

        // Load new level scene additively
        loadOperation = SceneManager.LoadSceneAsync(settings.levelName, LoadSceneMode.Additive);
        yield return loadOperation;

        // Apply settings once the scene is loaded
        Scene levelScene = SceneManager.GetSceneByName(settings.levelName);
        SceneManager.SetActiveScene(levelScene);

        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.ApplySettings(settings);
        }
    }

    public void LevelReset()
    {
        LevelManager levelManager = FindFirstObjectByType<LevelManager>();
        levelManager.LevelReset();
    }
}