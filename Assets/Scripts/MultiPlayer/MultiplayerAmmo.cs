using UnityEngine;
using Photon.Pun;

public class MultiplayerAmmo : MonoBehaviourPun
{

    public float ammoSpeed = 10f;
    public MultiPlayerPlayerMove playermove;
    private void Start()
    {
        playermove = FindAnyObjectByType<MultiPlayerPlayerMove>();
    }
    void Update()
    {
        // Liikuta ammusta ylöspäin
        transform.Translate(Vector3.up * ammoSpeed * Time.deltaTime);

        // Tuhotaan ammus automaattisesti tietyn ajan kuluttua
        if (photonView.IsMine) // Vain ammuksen omistaja käsittelee tuhoutumisen
        {
            Destroy(gameObject, 4f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine) return; // Vain omistaja käsittelee törmäykset

        if (collision.gameObject.CompareTag("Bunker") || collision.gameObject.CompareTag("Top"))
        {
            PhotonNetwork.Destroy(gameObject); // Tuhotaan ammus verkossa
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(photonView.IsMine) 
            {
               // playermove.TestPoinits();
            }
            Debug.Log("Osuma pelaajaan!");
            PhotonNetwork.Destroy(gameObject); // Tuhotaan ammus verkossa
            
        }
    }
    [PunRPC]
    public void PointsToPlayers()
    {
        playermove.points += 1;
    }
}
