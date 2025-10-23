using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"Triggered with {col.name}, tag: {col.tag}");
        if (col.CompareTag("Player"))
        {
            Debug.Log("Player collected item!");
            levelManager.CollectItem();
            Destroy(gameObject);
        }
    }
    
    
}
