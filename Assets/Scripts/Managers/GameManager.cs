using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    public int totalScore;
 

   
    public int enemyCount = 0;
    public Image[] lives;
    public int livesRemaining = 3;
    public static GameManager manager;
    public string currentLevel;
    public Scenes scenes;
    public bool isGameOver;
    
    //jokaista tasoa varten on muuttuja, muuttujan nimen pit�� olla sama kuin LoadlEVELSCRIPTISS� OLEVAN LEVELTOLOAD MUUTTUJAN ARVO
    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    
    private void Awake()
    {
        //singleton
        //katostaan onko manageria jo olemassa.
        if(manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        // Kuuntelee aina, kun Scene vaihtuu
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Poistaa kuuntelijan k�yt�st�
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hakee SceneManagerin
        scenes = FindFirstObjectByType<Scenes>();

        // Hakee Lives-Containerin ja sen lapsena olevat Lives-kuvat
        GameObject livesContainer = GameObject.Find("Lives");

        if (livesContainer != null)
        {
            lives = livesContainer.GetComponentsInChildren<Image>();
        }
        else
        {
            Debug.Log("Lives container not found in this scene!");
        }

        // P�ivit� el�mien UI
        UpdateLivesUI();
    }
    public void UpdateLivesUI()
    {
        if (lives != null)
        {
            for (int i = 0; i < lives.Length; i++)
            {
                // N�yt� vain k�ytett�viss� olevat el�m�t
                lives[i].enabled = i < livesRemaining;
            }
        }
    }


    void Start()
    {
        //UpdateEnemyCountText(); 
    }
    private void Update()
    {
        if (livesRemaining <= 0)
        {
            isGameOver = true; // Est�� toistuvan suorituksen
            PauseGame();
            //LOAD SCENE Terolla pelaajalla triggerin�.
           
           
            
        }

        if (Input.GetKeyDown(KeyCode.M)) 
        {
            SceneManager.LoadScene("MainMenu");
        }

     CheckVictoryCondition();
        
    }
    public void CheckVictoryCondition()
    {
        if(Level1 &&  Level2 && Level3)
        {
            currentLevel = "";
            manager.Level1 = false;
            manager.Level2 = false;
            manager.Level3 = false;
            SceneManager.LoadScene("GameDone");
        }
    }
    public void CallGameOver()
    {
        scenes.GameOver();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1; // Jatkaa pelin ajan kulkua
    }
 
    public void EnemyDestroyed()
    {
        enemyCount++;
        UpdateEnemyCountText(); 
    }

    private void UpdateEnemyCountText()
    {
      //  text.text = enemyCount.ToString(); 
    }
    public void RemoveLife()
    {
        if (livesRemaining > 0)
        {
            livesRemaining--; // V�henn� el�mien m��r��
            UpdateLivesUI();  // P�ivit� el�mien visuaalinen ilme
            PauseBriefly();   // Lis�t��n pieni tauko osuman j�lkeen
        }
    }


    public void TotalScore(int score)
    {
        totalScore += score;
    }
    IEnumerator PauseCoroutine()
    {
        Time.timeScale = 0; // Pys�yt� peli
        yield return new WaitForSecondsRealtime(0.3f); // Odota aina 0.3 sekuntia
        Time.timeScale = 1; // Jatka peli�
    }
    public void PauseGame()
    {
        Time.timeScale = 0; // Pys�yt� peli
        if(isGameOver == true)
        {
            CallGameOver();
        }
           


    }
    public void PauseBriefly()
    {
        StartCoroutine(PauseCoroutine());
    }
   


    public void Save()
    {
        Debug.Log("GAMESAVED");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.totalScore = totalScore;
        data.enemyCount = enemyCount;
        data.livesRemaining = livesRemaining;
        data.currentLevel = currentLevel;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        data.Level4 = Level4;
        bf.Serialize(file, data);
        file.Close();

    }

    public void Load()
    {
        //Katsotaan onko tallennettua tiedostao olemassa. jos on niin load tapahtuu
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //MIETI T�H�N JOS LAITAT LOAD NAPPULAN VASTA KUN ON LADATTAVAA DATAA ?
            Debug.Log("Game LOADED");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //siirret��n ladattu info GameManageriin;
            totalScore = data.totalScore;
            enemyCount = data.enemyCount;
            enemyCount = data.enemyCount;
            livesRemaining = data.livesRemaining;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
            Level4 = data.Level4;
            
        }
    }
    //toinen luokka joka voidaan serialioisda, pit�� sis�ll��n vain sen datan mit� seriaalioidaan

    [Serializable]
    class PlayerData
    {
        public int totalScore;
        public int enemyCount = 0;
        public int livesRemaining = 3;
        public string currentLevel;
        public bool Level1;
        public bool Level2;
        public bool Level3;
        public bool Level4;

    }


}
