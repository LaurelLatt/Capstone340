using ScreenControllers;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    [SerializeField] private Button button;
    [SerializeField] private Text levelNumber;
    [SerializeField] private SelectScreenController selectScreenController;

    private void Start()
    {
        Refresh();
    }

    public void OnButtonPressed()
    {
        selectScreenController.OnLevelSelected(levelIndex);
    }

    public void Refresh()
    {
        bool unlocked = SaveSystem.IsLevelUnlocked(levelIndex);
        button.interactable = unlocked;
        levelNumber.color = unlocked ? Color.black : Color.darkGray;
        
    }
}
