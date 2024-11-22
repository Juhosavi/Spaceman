using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 0.5f; // Vihollisen liikkumisnopeus
    private Vector2 direction = Vector2.right; // Alkusuunta oikealle'
    public GameObject[] enemys;
    public int randnum;
  

    private void Start()
    {
        randnum = Random.Range(3, 12);
        InvokeRepeating(nameof(ShootAmmo), randnum, randnum);
    }
    void Update()
    {
        // Liikuta vihollista suuntaan
        this.transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jos törmätään objektiin, jonka tagi on "Wall"
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
            Debug.Log("COLLISION SIENÄÄÄN");
        }
        if(collision.gameObject.CompareTag("EnemyTest"))
        {
            ChangeDirection();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
            Debug.Log("OSUTTU SEINÄÄN");
        }
    }

    private void ChangeDirection()
    {
        // Vaihda suunta ja liikuta hieman alaspäin
        direction *= -1; // Vaihda suunta
        Vector3 position = this.transform.position;
        position.y -= 0.5f; // Siirrä alaspäin
        this.transform.position = position;
    }
    public void ShootAmmo()
    {
        // Luo lista aktiivisista vihollisista
        GameObject lowestEnemy = null;

        // Etsi alin aktiivinen vihollinen
        for (int i = enemys.Length - 1; i >= 0; i--)
        {
            if (enemys[i] != null && enemys[i].activeInHierarchy)
            {
                lowestEnemy = enemys[i];
                break; // Poistu silmukasta, kun alin aktiivinen vihollinen on löydetty
            }
        }

        // Jos löytyy alin aktiivinen vihollinen, anna sen ampua
        if (lowestEnemy != null)
        {
            Enemy enemyScript = lowestEnemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.Shoot();
                randnum = Random.Range(3, 10);
            }
        }
    }

}
