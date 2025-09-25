using UnityEngine;

namespace ScreenControllers
{
    public class GameScreenController : MonoBehaviour
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

        public void ShowPauseMenu()
        {
            uiScreenManager.ShowPauseScreen();
        }

        public void GoToResults()
        {
            uiScreenManager.ShowResultsScreen();
        }
    }
}
