using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour
{
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.manager.currentLevel != "")
        {
            //joku taso p‰‰sty l‰pi
            GameObject.Find(GameManager.manager.currentLevel).GetComponent<LoadLevel>().Cleared(true);
            transform.position = GameObject.Find(GameManager.manager.currentLevel).transform.GetChild(0).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;
            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
        }
    }

}