using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GoToMainMenu : MonoBehaviour
{
    void Start()
    {
        GameManager.manager.currentLevel = "";
        GameManager.manager.Level1 = false;
        GameManager.manager.Level2 = false;
        GameManager.manager.Level3 = false;
        GameManager.manager.Level4 = false;
        GameManager.manager.livesRemaining = 3;
        GameManager.manager.totalScore = 0;
        GameManager.manager.isGameOver = false;
        GameManager.manager.ResumeGame();
        SceneManager.LoadScene("MainMenu");

    }

  
}