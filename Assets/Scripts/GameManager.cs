using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{

    public int totalScore;
    public int enemyCount = 0;
    public Image[] lives;
    public int livesRemaining = 3;
    public static GameManager manager;
    public string currentLevel;
    //jokaista tasoa varten on muuttuja, muuttujan nimen pitää olla sama kuin LoadlEVELSCRIPTISSÄ OLEVAN LEVELTOLOAD MUUTTUJAN ARVO
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
    void Start()
    {
        //UpdateEnemyCountText(); 
    }
    private void Update()
    {
        if (livesRemaining <= 0)
        {
            Debug.Log("GAME OVER!");
            
        }
        if(Input.GetKeyDown(KeyCode.M)) 
        {
            SceneManager.LoadScene("MainMenu");
        }
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
        livesRemaining -= 1;
        lives[livesRemaining].enabled = false;
    }
    public void TotalScore(int score)
    {
        totalScore += score;
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
            Debug.Log("Game LOADED");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //siirretään ladattu info GameManageriin;
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
    //toinen luokka joka voidaan serialioisda, pitää sisällään vain sen datan mitä seriaalioidaan

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
