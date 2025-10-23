using ScreenControllers;
using UnityEngine;

public class Shortcuts : MonoBehaviour
{
    public UIScreenManager uiScreenManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LoadLevel(1);
        }
    }
}
