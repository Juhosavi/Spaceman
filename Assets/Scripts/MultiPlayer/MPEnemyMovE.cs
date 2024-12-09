using UnityEngine;
using Photon.Pun;
using System.Linq;

public class MPEnemyMove : MonoBehaviourPun
{
    public float moveSpeed = 0.5f; // Vihollisen liikkumisnopeus
    private Vector2 direction = Vector2.right; // Alkusuunta oikealle
    public GameObject[] enemys; // Lista vihollisista
    public int randnum;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            randnum = Random.Range(3, 10);
            InvokeRepeating(nameof(ShootAmmo), randnum, randnum);
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            UpdateEnemyList(); // P�ivit� lista s��nn�llisesti
            CheckForEmptyList(); // Tarkista, onko lista tyhj�
        }
    }

    public void ShootAmmo()
    {
        if (!PhotonNetwork.IsMasterClient) return; // Vain Master Client suorittaa t�m�n

        UpdateEnemyList(); // P�ivit� lista ennen k�ytt��

        GameObject lowestEnemy = null;

        for (int i = enemys.Length - 1; i >= 0; i--)
        {
            if (enemys[i] != null && enemys[i].activeInHierarchy)
            {
                lowestEnemy = enemys[i];
                break;
            }
        }

        if (lowestEnemy != null)
        {
            PhotonView enemyPhotonView = lowestEnemy.GetComponent<PhotonView>();
            if (enemyPhotonView == null)
            {
                Debug.LogError("Viholliselta puuttuu PhotonView!");
                return;
            }

            photonView.RPC("ShootFromEnemy", RpcTarget.MasterClient, enemyPhotonView.ViewID);
        }

        randnum = Random.Range(3, 10); // P�ivitet��n uusi ammunnan ajastus
    }

    private void UpdateEnemyList()
    {
        enemys = enemys.Where(e => e != null && e.activeInHierarchy).ToArray();
    }

    private void CheckForEmptyList()
    {
        if (enemys.Length == 0)
        {
            Debug.Log($"{gameObject.name} tuhoutuu, koska enemys-lista on tyhj�.");
            PhotonNetwork.Destroy(gameObject); // Tuhoaa objektin Photonissa
        }
    }

    [PunRPC]
    public void ShootFromEnemy(int enemyViewID)
    {
        PhotonView enemyView = PhotonView.Find(enemyViewID);
        if (enemyView != null)
        {
            MultiPlayerEnemy enemyScript = enemyView.GetComponent<MultiPlayerEnemy>();
            if (enemyScript != null)
            {
                enemyScript.Shoot();
            }
            else
            {
                Debug.LogError("EnemyScript puuttuu PhotonView-instanssista!");
            }
        }
        else
        {
            Debug.LogError("PhotonView ID:ll� " + enemyViewID + " ei l�ytynyt!");
        }
    }
}
