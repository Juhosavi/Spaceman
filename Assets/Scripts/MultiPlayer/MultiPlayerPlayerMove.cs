using UnityEngine;
using Photon.Pun;
using TMPro;

public class MultiPlayerPlayerMove : MonoBehaviourPunCallbacks, IPunObservable
{
    public float moveSpeed;
    public GameObject ammoSpawn;
    public GameObject ammo;
    public GameManager manager;
    public bool canshoot;
    public Animator animator;
    public int killedEnemies;
    //public TextMeshPro killedField;
    public TextMeshPro nameField;
    public int points;
    public string playerName;
    private Vector3 targetScale; // Synkronoitu skaalan tila (k‰‰ntyminen)
    private Vector3 targetPosition; // Synkronoitu sijainti

    void Start()
    {
        //otetaan nimi data ja muutetaan se 
        object[] obj = photonView.InstantiationData;
        playerName = obj[0].ToString();
        nameField.text = playerName;

   
        if (photonView.IsMine)
        {
            manager = FindAnyObjectByType<GameManager>();
            canshoot = true;
            animator = GetComponent<Animator>();
        }
        else
        {
            targetPosition = transform.position;
            targetScale = transform.localScale; // Alustetaan synkronoitu skaala
        }
    }

    void Update()
    {
        
        //killedField.text = points.ToString();
        if (photonView.IsMine)
        {
            
            // Pelaajan liike
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

            // Rajoita liike alueelle -8 ja +8
            newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);
            transform.position = newPosition;

            // Hahmon flippaus
            if (horizontalInput > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // K‰‰nn‰ oikealle
            }
            else if (horizontalInput < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // K‰‰nn‰ vasemmalle
            }

            // P‰ivit‰ animaatio
            animator.SetBool("Run", horizontalInput != 0);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("ShootAmmo", RpcTarget.All);
                
            }
 
        }
        else
        {
            // Sujuvoita muiden pelaajien liike ja skaala
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 10f);
        }
    }
    [PunRPC]
    public void ShootAmmo()
    {
        if (photonView.IsMine)
        {
            // K‰yt‰ uutta moninpelin ammuksen prefabin nime‰
            PhotonNetwork.Instantiate("MultiplayerAmmo", ammoSpawn.transform.position, Quaternion.identity);
        }
    }
    [PunRPC]
    public void TestPoinits()
    {
        photonView.RPC("PointsToPlayers", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void PointsToPlayers()
    {
        points += 1;
    }




    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // L‰hetet‰‰n sijainti ja skaala (flippaus) verkkoon
            stream.SendNext(transform.position);
            stream.SendNext(transform.localScale);
        }
        else
        {
            // Vastaanotetaan sijainti ja skaala verkosta
            targetPosition = (Vector3)stream.ReceiveNext();
            targetScale = (Vector3)stream.ReceiveNext();
        }
    }
}
