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
    private int enemyCount = 0;
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

    }

    public void Load()
    {

    }
    //toinen luokka joka voidaan serialioisda, pitää sisällään vain sen datan mitä seriaalioidaan

    [Serializable]
    class PlayerData
    {
        public int totalScore;
        private int enemyCount = 0;
        public Image[] lives;
        public int livesRemaining = 3;
        public string currentLevel;
        public bool Level1;
        public bool Level2;
        public bool Level3;
        public bool Level4;

    }


}
