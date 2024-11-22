using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    public float ammoSpeed;
    public GameManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * ammoSpeed * Time.deltaTime);
        Destroy(gameObject, 4);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bunker"))
        {
            Destroy(gameObject);
            PlayerMovement playermove = FindFirstObjectByType<PlayerMovement>();
            playermove.canshoot = true;
        }
        if (collision.gameObject.CompareTag("PlayerHit"))
        {

            Debug.Log("Pelaajaan osuttu");
            manager.RemoveLife();
        }
        if (collision.gameObject.CompareTag("Top"))
        {
            Destroy(gameObject);
            PlayerMovement playermove = FindFirstObjectByType<PlayerMovement>();
            playermove.canshoot = true;
        }

    }

}
