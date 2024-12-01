using UnityEngine;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private List<RoomInfo> roomsList; //lista huoneita
    private const string roomName = "GameRoom";

    public GUIStyle myStyle;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myStyle.fontSize = 18;
        myStyle.normal.textColor = Color.white;
        roomsList = new List<RoomInfo>(); //alustetaan huonelistaus tyhj‰ll‰ listalla.

        //yhdistet‰‰n pilveen
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.IsConnectedAndReady.ToString());
        GUILayout.Label(PhotonNetwork.InLobby.ToString());

        if(PhotonNetwork.CurrentRoom != null)
        {
            GUILayout.Label(PhotonNetwork.CurrentRoom.ToString(), myStyle);
        }

        if(PhotonNetwork.InRoom == false)
        {
            //ei olla huoneessa mutta ollaan varmasti lobbyssa
            //annetaan pelaajan tehd‰ oma huone ja listataan kaikki huoneet

            if(GUI.Button(new Rect(100,100,250,100), "Create Room"))
            {
                //Create room painettu luodaan huone

                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
            }

            //listataan kaikki olemassa olveta huoneet. tehd‰‰n listaus vain jos huoneita on enemm‰n kuin 0
            if(roomsList.Count > 0) 
            {
                int i = 0;
                foreach(RoomInfo room in roomsList) 
                {

                    if (GUI.Button(new Rect(100, 250 * 110 * i, 250, 100), "Join: "+ room.Name))
                    {
                        PhotonNetwork.JoinRoom(room.Name);
                    }
                    i++;
                }
            }
        }
    }
    //p‰ivitet‰‰n paikallinen huonelistaus eli roomlist muuttuja. alla oleva funktio ajetaan kun pilvess‰ tapahtuu muutoksia huonelistaukseen
    //pilven huonelistaus on parametrina t‰lle funktiolle.

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //roomlist on meid‰n oma paikallinen lista huoneista. roomList on info joka tulee pilvest‰
        roomsList = roomList;
    }

    public override void OnConnectedToMaster()
    {
        //on saatu yhteys masteriin --> liityt‰‰n lobbyyn
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedRoom() 
    {
        //kun huoneeseen liityt‰‰n, instansioidaan kyseiselle k‰ytt‰j‰le pelihahmo pelimaaillmaan
        // kappale joka insansidoiaan PhotonNetworkin avulla, tulee sis‰lt‰‰ photonView komponentti
        //kappale tulisi olla myˆs Resources kansiossa.

        PhotonNetwork.Instantiate("SpaceManLatest", new Vector2(0, -4), Quaternion.identity, 0);
    }
}
