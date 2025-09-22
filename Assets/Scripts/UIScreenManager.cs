using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject selectScreen;
    public GameObject gameScreen;
    public GameObject resultsScreen;
    private GameObject activeScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScreen.SetActive(true);
        selectScreen.SetActive(false);
        gameScreen.SetActive(false);
        resultsScreen.SetActive(false);
        activeScreen = startScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowStartScreen()
    {
        ShowScreen(startScreen);
    }

    public void ShowSelectScreen()
    {
        ShowScreen(selectScreen);
    }

    public void ShowGameScreen()
    {
        ShowScreen(gameScreen);
    }

    public void ShowResultsScreen()
    {
        ShowScreen(resultsScreen);
    }

    private void ShowScreen(GameObject screenToShow)
    {
        screenToShow.SetActive(true);
        
        if (activeScreen != null)
            activeScreen.SetActive(false);

        screenToShow.SetActive(true);
        activeScreen = screenToShow;
        
    }
}
