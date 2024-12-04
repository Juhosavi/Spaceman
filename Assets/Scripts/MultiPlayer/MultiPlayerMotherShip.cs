using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MotherShipMovement : MonoBehaviourPun
{
    public float moveSpeed;

    void Update()
    {
        // Liikuta objektia oikealle tai vasemmalle
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Tarkista, onko kaikki lapset tuhottu
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene("Map"); // Lataa uusi scene
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveSpeed *= -1; // Vaihda suunta
      
        }
    }
}
