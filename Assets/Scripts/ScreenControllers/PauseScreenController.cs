using UnityEngine;

namespace ScreenControllers
{
    public class PauseScreenController : MonoBehaviour
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

        public void GoToOptionsMenu()
        {
            uiScreenManager.ShowOptionsScreen();
        }

        public void ResumeGame()
        {
            uiScreenManager.HidePauseScreen();
        }

        public void RestartLevel()
        {
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LevelReset();
        }

        public void GoToSelectScreen()
        {
            uiScreenManager.ShowSelectScreen();
        }
    }
}
