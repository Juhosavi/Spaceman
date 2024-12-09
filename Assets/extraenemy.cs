using System.Collections;
using UnityEngine;

public class extraenemy : MonoBehaviour
{
    public GameObject projectile; // Ammuksen prefab
    public GameObject ammoSpawn; // Ammuksen lähtöpiste
    public GameManager manager; // Viittaus GameManageriin
    public PointManager pointManager; // Viittaus PointManageriin
    public float moveDistance = 50f; // Kuinka pitkälle liikutaan kumpaankin suuntaan
    public float moveSpeed = 5f; // Nopeus liikkumiseen
    private Vector3 startPosition; // Alkuperäinen sijainti
    private bool movingRight = true; // Liikesuunta

    void Start()
    {
        // Tallenna alkuperäinen sijainti ja hae managerit
        startPosition = transform.position;
        pointManager = FindAnyObjectByType<PointManager>();
        manager = FindAnyObjectByType<GameManager>();

        // Aloita ampuminen satunnaisin väliajoin
        StartCoroutine(ShootingCoroutine());
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Liikuta vihollista edestakaisin
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                movingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // Pelaajan ampuma osuu, anna pelaajalle mahdollisuus ampua uudelleen
            PlayerMovement playermove = FindFirstObjectByType<PlayerMovement>();
            playermove.canshoot = true;

            // Päivitä pisteet ja tuhoa vihollinen
            pointManager.UpdateScore(100);
            Destroy(gameObject);
            Destroy(collision.gameObject);


            Debug.Log("Pelaaja osui extra-viholliseen!");
        }
    }

    public void Shoot()
    {
        // Luo ammuksen
        Instantiate(projectile, ammoSpawn.transform.position, Quaternion.identity);
    }

    private IEnumerator ShootingCoroutine()
    {
        while (true)
        {
            // Satunnainen viive ennen ampumista
            yield return new WaitForSeconds(Random.Range(1f, 12f));
            Shoot();
        }
    }
}
