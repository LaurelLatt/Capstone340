using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenControllers
{
    public class GameScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;
        [SerializeField] private Text collectedText;

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

        public void ShowCollectedText()
        {
            collectedText.gameObject.SetActive(true);
        }

        public void HideCollectedText()
        {
            collectedText.gameObject.SetActive(false);
        }

        public void UpdateCollectibleUI(int collected)
        {
            collectedText.text = "Collected: " + collected;
        }

        // private bool inCombat;
        //
        //
        // private IEnumerator GenerateHealth()
        // {
        //     
        // }
        
    }
}
