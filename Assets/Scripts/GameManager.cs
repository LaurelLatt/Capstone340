using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public static GameManager Instance { get; private set; }
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

    public void LevelReset()
    {
        player.ResetPosition();
    }

    public void StartGame()
    {
        player.ResetPosition();
    }

    public void OnLevelLoaded(GameObject currentLevel)
    {
        LevelData levelData = currentLevel.GetComponent<LevelData>();
        if (levelData != null)
        {
            player.transform.position = levelData.playerSpawn.position;
        }
        Time.timeScale = 1;
        
    }
}
