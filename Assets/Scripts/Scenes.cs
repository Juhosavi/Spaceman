using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Map");
    }
    public void Save()
    {
        //tämä ajetaan menusta kun painetaan save painiketta
        GameManager.manager.Save();
    }
    public void Load()
    {
        GameManager.manager.Load();
    }
}
