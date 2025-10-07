using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform levelParent; // LevelContainer
    [SerializeField] private GameObject[] levelPrefabs;

    private GameObject currentLevel;

    public void LoadLevel(int index)
    {
        // Remove previous level
        if (currentLevel != null)
            Destroy(currentLevel);

        // Instantiate new level prefab
        currentLevel = Instantiate(levelPrefabs[index], levelParent);

        // Notify GameManager (optional)
        GameManager.Instance.OnLevelLoaded(currentLevel);
    }
}
