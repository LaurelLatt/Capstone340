using UnityEngine;

public class Collectible : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            levelManager.CollectItem();
            gameObject.SetActive(false);
        }
    }
    
    
}
