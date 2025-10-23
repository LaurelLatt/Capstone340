using UnityEngine;


[CreateAssetMenu(fileName = "NewLevelSettings", menuName = "Game/Level Settings")]
public class LevelSettings : ScriptableObject
{
    public Vector3 spawnPosition;
    public bool hasCollectibles;
    public int totalCollectibles;
    public string levelName;
    public bool heightInversionEnabled;
}
