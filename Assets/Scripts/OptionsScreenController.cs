using UnityEngine;

public class OptionsScreenController : MonoBehaviour
{
    public UIScreenManager uiScreenManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Exit()
    {
        uiScreenManager.HideOptionsScreen();
    }

    public void GoToSelectScreen()
    {
        uiScreenManager.ShowSelectScreen();
    }
}
