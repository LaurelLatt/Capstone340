using UnityEngine;

namespace ScreenControllers
{
    public class SelectScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;
        public GameManager gameManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void OnLevelSelected(int levelIndex)
        {
            Debug.Log("Level " + levelIndex + " selected");
            // Later: store this index in a GameManager or LevelManager
            uiScreenManager.ShowGameScreen();
            gameManager.StartGame();
        }

        public void GoToOptionsMenu()
        {
            uiScreenManager.ShowOptionsScreen();
        }
    }
}