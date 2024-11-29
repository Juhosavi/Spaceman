using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject projectile;
    public GameObject ammoSpawn;
    public GameManager manager;
    public PointManager pointManager;
    [SerializeField] private AudioClip[] EnemyDamageaudioClips;
    



    void Start()
    {
        
        pointManager = FindAnyObjectByType<PointManager>();
        manager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            PlayerMovement playermove = FindFirstObjectByType<PlayerMovement>();
            playermove.canshoot = true;
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Debug.Log("Pelaaja osui");
            pointManager.UpdateScore(10);
            //SoundFXManager.Instance.PlaySoundEffect(audioClip, transform, 1f);
            SoundFXManager.Instance.PlayRandomSoundEffect(EnemyDamageaudioClips, transform, 1f);
            
        }
        if(collision.gameObject.CompareTag("AlienWin"))
        {
            Debug.Log("ALIENIT VOITTI");
            manager.PauseGame();
            manager.CallGameOver();
        }

    }
    public void Shoot()
    {
        
        Instantiate(projectile,ammoSpawn.transform.position, Quaternion.identity);
    }
}
