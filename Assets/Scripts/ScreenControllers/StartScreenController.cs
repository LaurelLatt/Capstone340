using UnityEngine;

namespace ScreenControllers
{
    public class StartScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        public void OnStartButtonClicked()
        {
            uiScreenManager.ShowSelectScreen();
        }
    }
}