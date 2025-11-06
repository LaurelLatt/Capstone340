using ScreenControllers;
using UnityEngine;

public class Shortcuts : MonoBehaviour
{
    public UIScreenManager uiScreenManager;
    [SerializeField] private SelectScreenController selectScreenController;
    
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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LoadLevel(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LoadLevel(3);
        }
    }

    public void UnlockAllLevels()
    {
        int numLevels = GameManager.Instance.allLevels.Length - 1;
        for (int i = 0; i <= numLevels; i++)
        {
            SaveSystem.UnlockLevel(i);
        }
        selectScreenController.RefreshButtons();
    }

    public void ResetProgress()
    {
        SaveSystem.ResetProgress();
        selectScreenController.RefreshButtons();
    }
}
