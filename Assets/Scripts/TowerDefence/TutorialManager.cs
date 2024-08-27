using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI; 

    private void Start()
    {
        ShowTutorial(); // Show the tutorial UI at the start
    }

    public void ShowTutorial()
    {
        tutorialUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game to focus on the tutorial
    }

    public void HideTutorial()
    {
        tutorialUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }
    public bool IsTutorialVisible()
    {
        return tutorialUI.activeSelf; // Return the current visibility state of the tutorial UI
    }
}
