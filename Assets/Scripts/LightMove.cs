using UnityEngine;
using UnityEngine.SceneManagement;

public class LightMove : MonoBehaviour
{
    public float moveSpeed;
    public BoxCollider2D boxCollider; // Viittaus BoxCollider2D-komponenttiin
    private bool[] destroyedChildren; // Seurataan tuhottuja lapsia
    private int totalChildren = 8; // Yhteens‰ 8 lasta (0-7)

    void Start()
    {
        // Alustetaan taulukko lasten tuhoutumisen seuraamiseksi
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
                OnChildDestroyed(i); // K‰sitell‰‰n lapsen vaikutus BoxCollideriin
            }
        }

        // Tarkista, onko kaikki lapset tuhottu
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene("Map");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            moveSpeed *= -1; // Vaihda suunta
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }

    void OnChildDestroyed(int childIndex)
    {
        if (boxCollider != null)
        {
            Vector2 newSize = boxCollider.size;
            Vector2 newOffset = boxCollider.offset;

            // Jos lapsi on oikealta puolen 3 ensimm‰ist‰ (esim. child0, child1, child2)
            if (childIndex <= 2)
            {
                newSize.x -= 1; // Pienennet‰‰n BoxColliderin leveytt‰
                newOffset.x -= 0.5f; // Siirret‰‰n offset vasemmalle (kasautuu oikealle)
                Debug.Log($"Right-side child{childIndex} destroyed: BoxCollider resized!");
            }
            // Jos lapsi on vasemmalta puolen 3 viimeist‰ (esim. child5, child6, child7)
            else if (childIndex >= 5)
            {
                newSize.x -= 1; // Pienennet‰‰n BoxColliderin leveytt‰
                newOffset.x += 0.5f; // Siirret‰‰n offset oikealle (kasautuu vasemmalle)
                Debug.Log($"Left-side child{childIndex} destroyed: BoxCollider resized!");
            }

            boxCollider.size = newSize;
            boxCollider.offset = newOffset;

            Debug.Log($"child{childIndex} destroyed: BoxCollider resized!");
        }
    }
}
