using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Ended!");
        // You can add more end game logic here
        // For example, load a "Game Over" scene:
        // SceneManager.LoadScene("GameOverScene");

        // Or quit the application (in builds, not in editor):
        // Application.Quit();
    }
}
