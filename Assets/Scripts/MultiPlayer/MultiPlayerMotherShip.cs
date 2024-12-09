using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MotherShipMovement : MonoBehaviourPun
{
    public float moveSpeed;
    private bool[] destroyedChildren; // Seurataan tuhottuja lapsia
    private int totalChildren;

    void Start()
    {
        // Alustetaan tuhottujen lasten taulukko
        totalChildren = transform.childCount; // Alustetaan lasten lukum‰‰r‰
        destroyedChildren = new bool[totalChildren];
    }

    void Update()
    {
        // Liikuta objektia oikealle tai vasemmalle
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Tarkista jokainen lapsi
        for (int i = 0; i < totalChildren; i++)
        {
            Transform child = transform.Find("child" + i);
            if (child == null && !destroyedChildren[i]) // Jos lapsi on tuhottu mutta ei viel‰ k‰sitelty
            {
                destroyedChildren[i] = true; // Merkit‰‰n lapsi tuhottavaksi
                OnChildDestroyed(i); // Kutsutaan lapsen tuhoutumisen k‰sittelij‰
            }
        }

        // Tarkista, onko kaikki lapset tuhottu
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene("MPGameDone"); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveSpeed *= -1; // Vaihda suunta
        }
    }

    void OnChildDestroyed(int childIndex)
    {
        Debug.Log($"Child {childIndex} destroyed.");
        // Voit lis‰t‰ t‰nne muita k‰sittelyj‰, jos haluat.
    }
}
