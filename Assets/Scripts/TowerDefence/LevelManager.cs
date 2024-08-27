using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;
    public GameOverManager gameOverManager;

    public Transform startPoint;
    public Transform[] path;

    public int currency;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        currency = 200;
    }

    public void IncreaseCurrency(int amount) {
        currency += amount;
    }

    public bool SpendCurrency(int amount) {
        if(amount <= currency) {
            currency -= amount;
            return true;
        } else {
            Debug.Log("Insufficient Currency!");
            return false;
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over...");
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
        else
        {
            Debug.LogError("GameOverManager is not assigned in LevelManager.");
        }
    }
}
