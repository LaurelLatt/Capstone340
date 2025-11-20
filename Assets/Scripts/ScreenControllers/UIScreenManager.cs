using UnityEngine;

namespace ScreenControllers
{
    public class UIScreenManager : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject selectScreen;
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject resultsScreen;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject optionsScreen;
        [SerializeField] private SelectScreenController selectScreenController;
        
        private GameObject activeScreen;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ShowStartScreen();
            DebugLogger.SetLevel(LogChannel.Gameplay, LogLevel.Info);
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
            selectScreenController.RefreshButtons();
            GameManager.Instance.UnloadLevelAsync();
            ShowScreen(selectScreen);
        }

        public void ShowGameScreen()
        {
            ShowScreen(gameScreen);
        }

        public void ShowResultsScreen()
        {
            ShowScreen(resultsScreen);
            GameManager.Instance.UnloadLevelAsync();
        }

        public void ShowPauseScreen()
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            
            
        }

        public void HidePauseScreen()
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            
        }

        public void ShowOptionsScreen()
        {
            optionsScreen.SetActive(true);
        }

        public void HideOptionsScreen()
        {
            optionsScreen.SetActive(false);
        }

        private void ShowScreen(GameObject screenToShow)
        {
            HideAllScreens();
            screenToShow.SetActive(true);

            if (activeScreen != null)
                activeScreen.SetActive(false);

            screenToShow.SetActive(true);
            activeScreen = screenToShow;

        }

        private void HideAllScreens()
        {
            startScreen.SetActive(false);
            selectScreen.SetActive(false);
            gameScreen.SetActive(false);
            resultsScreen.SetActive(false);
            pauseScreen.SetActive(false);
            optionsScreen.SetActive(false);
        }
    }
}