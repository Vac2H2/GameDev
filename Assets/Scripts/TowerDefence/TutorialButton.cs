using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private TMP_Text buttonText; // Reference to the TMP_Text component for the button label

    private Button button;
    private bool isTutorialVisible = false;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found!");
            return;
        }

        button.onClick.AddListener(OnButtonClick); // Add a listener

        // Initialize button text
        UpdateButtonText();
    }

    private void Start()
    {
        if (tutorialManager != null)
        {
            isTutorialVisible = tutorialManager.IsTutorialVisible();
            UpdateButtonText(); // Update the button text based on the initial state
        }
        else
        {
            Debug.LogError("TutorialManager reference is missing!");
        }
    }

    private void OnButtonClick()
    {
        if (tutorialManager != null)
        {
            if (isTutorialVisible)
            {
                tutorialManager.HideTutorial(); // Hide the tutorial if it is currently visible
            }
            else
            {
                tutorialManager.ShowTutorial(); // Show the tutorial if it is currently hidden
            }
            isTutorialVisible = !isTutorialVisible; // Toggle the visibility state

            // Update button text
            UpdateButtonText();
        }
        else
        {
            Debug.LogError("TutorialManager reference is missing!");
        }
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
        {
            buttonText.text = isTutorialVisible ? "Close" : "Help"; // Set text based on tutorial visibility
        }
        else
        {
            Debug.LogError("TMP_Text component reference is missing!");
        }
    }
}
