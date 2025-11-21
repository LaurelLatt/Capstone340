using UnityEngine;


[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public Vector3 spawnPosition;
    public string levelName;
    
    [Header("Collectible Items")]
    public bool hasCollectibles;
    [Tooltip("Number of collectibles needed to pass level.")]
    public int goalCollectibles;
    [Tooltip("Check if collectibles are only necessary win condition")]
    public bool collectiblesAsOnlyWinCondition;
    [Tooltip("Check if there is another win condition, too.")]
    public bool collectiblesAsAWinCondition;
    
    [Header("Gravity Settings")]
    public bool heightInversionEnabled;
    
    [Header("Camera Fade Variables")]
    public bool hasCameraFade;
    public float fadeWaitTime = 0;

    [Header("Player Settings")] 
    [Tooltip("This freezes the player for camera fade mechanic.")]
    public bool playerFreeze;

    [Tooltip("This sets the bounds for the level. If player passes these points, they reset.")]
    public float upperBound = 20f;
    public float lowerBound = -10f;
}
