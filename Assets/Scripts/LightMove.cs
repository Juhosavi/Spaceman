using UnityEngine;
using UnityEngine.SceneManagement;

public class LightMove : MonoBehaviour
{
    public float moveSpeed;


    void Update()
    {
        // Liikuta objektia oikealle tai vasemmalle
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        
        //// Tarkista, osuuko objekti ruudun oikeaan tai vasempaan reunaan
        //Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        //if (screenPosition.x >= 1.0f || screenPosition.x <= 0.0f)
        //{
        //    moveSpeed *= -1; // Vaihda suunta
        //}
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene("Map");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            moveSpeed *= -1; // Vaihda suunta
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }
}
