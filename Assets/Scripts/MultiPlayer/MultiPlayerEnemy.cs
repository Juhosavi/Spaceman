using UnityEngine;
using Photon.Pun;

public class MultiPlayerEnemy : MonoBehaviour
{
    public GameObject projectile; // Ammuksen prefab
    public GameObject ammoSpawn;  // Ammuksen lähtöpaikka
    public GameManager manager;
    public PointManager pointManager;
    [SerializeField] private AudioClip[] EnemyDamageaudioClips;

    void Start()
    {
        pointManager = FindAnyObjectByType<PointManager>();
        manager = FindAnyObjectByType<GameManager>();

        // Tarkista omistajuus, jos vihollinen on skenessä valmiina
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView != null && photonView.IsSceneView)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // Tuhotaan vihollinen
            DestroyEnemy();

            // Tarkista ensin, että projektiili ei ole null
            if (collision.gameObject != null)
            {
                PhotonView projectilePhotonView = collision.gameObject.GetComponent<PhotonView>();
                if (projectilePhotonView != null && projectilePhotonView.IsMine)
                {
                    PhotonNetwork.Destroy(collision.gameObject);
                }
            }

            // Päivitä pisteet, jos Master Client
            if (PhotonNetwork.IsMasterClient)
            {
               // pointManager.UpdateScore(10);
            }

            Debug.Log("Pelaaja osui");
            SoundFXManager.Instance.PlayRandomSoundEffect(EnemyDamageaudioClips, transform, 1f);
        }

        if (collision.gameObject.CompareTag("AlienWin"))
        {
            Debug.Log("ALIENIT VOITTI");
            manager.PauseGame();
            manager.CallGameOver();
        }
    }


    public void Shoot()
    {
        if (!PhotonNetwork.IsMasterClient) return; // Vain Master Client luo ammuksen

        if (ammoSpawn == null || projectile == null)
        {
            Debug.LogError("Ammuksen lähtöpaikka tai prefab puuttuu!");
            return;
        }

        // Luo ammuksen
        PhotonNetwork.Instantiate(projectile.name, ammoSpawn.transform.position, Quaternion.identity);
    }

    public void DestroyEnemy()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView != null)
        {
            if (!photonView.IsMine)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    // Master Client ottaa omistajuuden
                    photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
                }
                else
                {
                    Debug.LogError("Pelaajalla ei ole oikeuksia tuhota tätä objektia.");
                    return;
                }
            }

            PhotonNetwork.Destroy(gameObject); // Tuhotaan objekti
        }
        else
        {
            Debug.LogError("PhotonView puuttuu viholliselta!");
        }
    }

    [PunRPC]
    public void RequestDestroyEnemy(int photonViewID)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonView targetView = PhotonView.Find(photonViewID);
            if (targetView != null)
            {
                PhotonNetwork.Destroy(targetView.gameObject);
            }
            else
            {
                Debug.LogError("PhotonView ID:tä ei löydetty.");
            }
        }
    }
}
