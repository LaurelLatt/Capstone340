using UnityEngine;


[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public Vector3 spawnPosition;
    public string levelName;
    
    [Header("Collectible Items")]
    public bool hasCollectibles;
    public int totalCollectibles;
    public bool collectiblesAsWinCondition;
    
    [Header("Gravity Settings")]
    public bool heightInversionEnabled;
    
    [Header("Camera Fade Variables")]
    public bool hasCameraFade;
    public float fadeWaitTime = 0;
}
