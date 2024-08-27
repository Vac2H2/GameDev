using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        if (gameOverPanel == null || mainMenuButton == null)
        {
            Debug.LogError("GameOverManager UI elements are not assigned!");
            return;
        }

        gameOverPanel.SetActive(false);

        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void ShowGameOver()
    {
        Debug.Log("Showing Game Over Panel");
        gameOverPanel.SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0); // Ensure Scene 0 is correctly set as the main menu
    }
}