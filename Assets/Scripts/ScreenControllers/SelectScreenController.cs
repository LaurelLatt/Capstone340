using UnityEngine;

namespace ScreenControllers
{
    public class SelectScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;
        public LevelManager levelManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void OnLevelSelected(int levelIndex)
        {
            DebugLogger.Log(LogChannel.Gameplay, "Level " + levelIndex + " selected");
            // Later: store this index in a GameManager or LevelManager
            uiScreenManager.ShowGameScreen();
            GameManager.Instance.LoadLevel(levelIndex);
        }

        public void GoToOptionsMenu()
        {
            uiScreenManager.ShowOptionsScreen();
        }
    }
}