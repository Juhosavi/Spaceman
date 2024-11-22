using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Enemy[] prefabs; // Vihollisten prefab-lista
    public EnemyAmmo missilePrefab; // Vihollisten ammuksen prefab
    public int rows = 5;
    public int columns = 10;
    public float moveSpeed = 0.3f;
    public float missileSpawnRate = 1f; // Ammusten ampumisväli
    private Vector2 direction = Vector2.right;

    private void Awake()
    {
        // Luo vihollisryhmä ruudukkoon
        for (int row = 0; row < this.rows; row++)
        {
            float width = 1.0f * (this.columns - 1);
            float height = 1.0f * (this.rows - 1);
            Vector3 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 1.0f), 0.0f);
            for (int col = 0; col < this.columns; col++)
            {
                Enemy enemy = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 1.0f;
                enemy.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        // Aloita ampuminen tietyllä aikavälillä
        InvokeRepeating(nameof(MissileAttack), missileSpawnRate, missileSpawnRate);
    }

    void Update()
    {
        // Liikuta koko vihollisryhmää yhdessä
        this.transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

        // Tarkista, osuuko joku vihollinen ruudun reunaan
        foreach (Transform enemy in this.transform)
        {
            if (!enemy.gameObject.activeInHierarchy) continue;

            if (direction == Vector2.right && enemy.position.x >= Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
            {
                ChangeDirection();
                break;
            }
            else if (direction == Vector2.left && enemy.position.x <= Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
            {
                ChangeDirection();
                break;
            }
        }
    }

    private void ChangeDirection()
    {
        // Käännä koko vihollisryhmän suunta ja liikuta alaspäin
        direction *= -1;
        Vector3 position = this.transform.position;
        position.y -= 0.5f; // Siirrä alaspäin
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        // Luo lista alimmista vihollisista jokaisessa sarakkeessa
        List<Transform> lowestEnemies = new List<Transform>();

        for (int col = 0; col < columns; col++)
        {
            Transform lowestEnemy = null;

            // Etsi alin aktiivinen vihollinen kussakin sarakkeessa
            for (int row = 0; row < rows; row++)
            {
                int index = row * columns + col;

                if (index < transform.childCount)
                {
                    Transform enemy = transform.GetChild(index);

                    if (enemy.gameObject.activeInHierarchy)
                    {
                        lowestEnemy = enemy; // Tallenna alin aktiivinen vihollinen
                    }
                }
            }

            if (lowestEnemy != null)
            {
                lowestEnemies.Add(lowestEnemy); // Lisää listaaan alin vihollinen sarakkeessa
            }
        }

        // Valitse satunnainen vihollinen listasta ampumaan
        if (lowestEnemies.Count > 0)
        {
            Transform randomEnemy = lowestEnemies[Random.Range(0, lowestEnemies.Count)];
            EnemyAmmo missile = Instantiate(missilePrefab, randomEnemy.position, Quaternion.identity);
            missile.ammoSpeed = 5f; // Ammuksen nopeus
        }
    }


}


