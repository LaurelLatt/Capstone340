using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            levelManager.LevelComplete();
        }
    }
}
