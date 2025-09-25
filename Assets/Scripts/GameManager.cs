using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelReset()
    {
        player.ResetPosition();
    }

    public void StartGame()
    {
        player.ResetPosition();
    }
}
