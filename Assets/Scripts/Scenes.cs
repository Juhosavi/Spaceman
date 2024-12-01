using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public Image pause;
    public Image gameOver;
    public bool isPaused = false;
    public GameObject soundPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Map");
    }
    public void Save()
    {
        //t‰m‰ ajetaan menusta kun painetaan save painiketta
        GameManager.manager.Save();
    }
    public void Load()
    {
        GameManager.manager.Load();
        
    }
    public void RestartScene()
    {
        // Hakee nykyisen kohtauksen nimen
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Lataa nykyisen kohtauksen uudelleen
        SceneManager.LoadScene(currentSceneName);
    }
    public void PauseGame()
    {
        Time.timeScale = 0; // Pys‰ytt‰‰ pelin ajan
        isPaused = true;
        pause.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Jatkaa pelin ajan kulkua
        isPaused = false;
        pause.gameObject.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Loading");
        
    }
    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OptionsButton()
    {
        soundPanel.gameObject.SetActive(true);
    }
    public void ReturnMainmenuPanel()
    {
        soundPanel.gameObject.SetActive(false);
    }
    public void MultiPlayerBUtton()
    {
        SceneManager.LoadScene("PhotonMultiplayer");
    }
}
