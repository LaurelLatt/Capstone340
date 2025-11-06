using UnityEngine;

public static class SaveSystem
{
    private const string LevelUnlockKey = "LevelUnlocked_";

    public static void UnlockLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(LevelUnlockKey + levelIndex, 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex == 0) return true;
        
        return PlayerPrefs.GetInt(LevelUnlockKey + levelIndex, 0) == 1;
    }
    
    public static void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
    
}
