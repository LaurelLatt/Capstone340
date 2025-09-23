using UnityEngine;
using UnityEngine.UI;

public class ResultsScreenController : MonoBehaviour
{
    public UIScreenManager uiScreenManager;
    public Text resultsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayResults();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayResults()
    {
        resultsText.text = "You won!";
        
    }

    public void RetryLevel()
    {
        uiScreenManager.ShowGameScreen();
    }

    public void GoToSelectScreen()
    {
        uiScreenManager.ShowSelectScreen();
    }
}
