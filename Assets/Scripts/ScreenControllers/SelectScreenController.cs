using System.Collections.Generic;
using UnityEngine;

namespace ScreenControllers
{
    public class SelectScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;
        [SerializeField] private List<LevelButton> levelButtons;
        

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

        public void RefreshButtons()
        {
            foreach (LevelButton button in levelButtons)
            {
                button.Refresh();
            }
        }
    }
}