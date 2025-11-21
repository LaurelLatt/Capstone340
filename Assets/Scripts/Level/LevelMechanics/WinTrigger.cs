using System.Collections;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Collider2D triggerCollider;
    
    private void OnEnable()
    {
        LevelManager.OnLevelReset += EnableTriggerWithDelay;
    }

    private void OnDisable()
    {
        LevelManager.OnLevelReset -= EnableTriggerWithDelay;
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            levelManager.LevelComplete();
            triggerCollider.enabled = false;
        }
    }

    private void EnableTrigger()
    {
        triggerCollider.enabled = true;
    }

    private void EnableTriggerWithDelay()
    {
        StartCoroutine(ReenableWinTrigger());
    }
    private IEnumerator ReenableWinTrigger()
    {
        yield return null;
        EnableTrigger();
    }
    
}
