using UnityEngine;

public static class SaveSystem
{
    private const string LevelUnlockKey = "LevelUnlocked_";
    private const string AudioKey = "Audio_";

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

    public static void SaveMusicMuteState(bool muted)
    {
        PlayerPrefs.SetInt(AudioKey + "musicMuted", muted ? 1 : 0);
    }

    public static bool LoadMusicMuteState()
    {
        return PlayerPrefs.GetInt(AudioKey + "musicMuted", 0) == 1;
    }

    public static void SaveMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(AudioKey + "musicVolume", volume);
    }

    public static float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat(AudioKey + "musicVolume", 0.5f);
    } 

    public static void SaveSoundMuteState(bool muted)
    {
        PlayerPrefs.SetInt(AudioKey + "soundMuted", muted ? 1 : 0);
    }

    public static bool LoadSoundMuteState()
    {
        return PlayerPrefs.GetInt(AudioKey + "soundMuted", 0) == 1;
    }
}
