using UnityEngine;

public class UIScreenManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject selectScreen;
    public GameObject gameScreen;
    public GameObject resultsScreen;
    public GameObject pauseScreen;
    private GameObject activeScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideAllScreens();
        ShowStartScreen();
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

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    private void ShowScreen(GameObject screenToShow)
    {
        screenToShow.SetActive(true);
        
        if (activeScreen != null)
            activeScreen.SetActive(false);

        screenToShow.SetActive(true);
        activeScreen = screenToShow;
        
    }

    private void HideAllScreens()
    {
        startScreen.SetActive(true);
        selectScreen.SetActive(false);
        gameScreen.SetActive(false);
        resultsScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
}
