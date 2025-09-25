using ScreenControllers;
using UnityEngine;

public class Shortcuts : MonoBehaviour
{
    public UIScreenManager uiScreenManager;
    public GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            uiScreenManager.ShowGameScreen();
            gameManager.StartGame();
        }
    }
}
