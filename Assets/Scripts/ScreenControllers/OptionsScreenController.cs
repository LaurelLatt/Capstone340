using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenControllers
{
    public class OptionsScreenController : MonoBehaviour
    {
        public UIScreenManager uiScreenManager;

        public Image musicButtonImage;
        public Image soundButtonImage;
        
        public Sprite musicOnSprite;
        public Sprite musicOffSprite;
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Exit()
        {
            uiScreenManager.HideOptionsScreen();
        }

        public void GoToSelectScreen()
        {
            uiScreenManager.ShowSelectScreen();
        }

        public void QuitGame()
        {
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #else
                Application.Quit(); // For standalone builds
            #endif
            
        }

        public void SwapMusicSprite()
        {
            musicButtonImage.sprite = musicButtonImage.sprite == musicOnSprite ? musicOffSprite : musicOnSprite;
        }

        public void SwapSoundSprite()
        {
            soundButtonImage.sprite = soundButtonImage.sprite == soundOnSprite ? soundOffSprite : soundOnSprite;
        }
    }
}