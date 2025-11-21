using System.Collections.Generic;
using UnityEngine;

public class CollectibleCountBox : MonoBehaviour
{
    public LevelManager levelManager;
    public List<SpriteRenderer> collectibleSprites;

    private void OnEnable()
    {
        LevelManager.OnCollectibleCollected += HandleCollectibleEvent;
        LevelManager.OnLevelReset += ResetSpriteState;
    }

    private void OnDisable()
    {
        LevelManager.OnCollectibleCollected -= HandleCollectibleEvent;
        LevelManager.OnLevelReset -= ResetSpriteState;
    }
    
    private void Start()
    {
        foreach (var cSprite in collectibleSprites)
        {
            cSprite.color = Color.gray;
        }
    }

    private void HandleCollectibleEvent()
    {
        if (levelManager == null)
            levelManager = FindFirstObjectByType<LevelManager>();

        UnlockCollectibles(levelManager.collectedItems);
        
    }

    public void UnlockCollectibles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            collectibleSprites[i].color = Color.white;
        }
    }

    private void ResetSpriteState()
    {
        foreach (var cSprite in collectibleSprites)
        {
            cSprite.color = Color.gray;
        }
        
    }
    
}
