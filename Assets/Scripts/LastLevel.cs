using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LastLevel : MonoBehaviour
{
    public TextMeshProUGUI lastPoints; // Huomaa TextMeshProUGUI!
    public GameManager manager;

    // Start is called kerran ennen ensimmäistä Update-suoritusta
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
    }

  
    void Update()
    {
        if (manager != null && lastPoints != null)
        {
            lastPoints.text = manager.totalScore.ToString(); 
        }
    }
    public void ReturnFromMenu()
    {
        SceneManager.LoadScene("Loading");
    }
}
