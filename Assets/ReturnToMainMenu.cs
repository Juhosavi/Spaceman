using UnityEngine;
using Photon.Pun;

public class ReturnToMainMenu : MonoBehaviourPunCallbacks
{
    // Funktio huoneesta poistumiseen
    public void LeaveRoomManually()
    {
        if (PhotonNetwork.InRoom)
        {
            Debug.Log("Leaving room...");
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            Debug.Log("Not in a room, no need to leave.");
        }
    }

    // Funktio yhteyden katkaisemiseen
    public void DisconnectManually()
    {
        if (PhotonNetwork.IsConnected)
        {
            Debug.Log("Disconnecting from Photon...");
            PhotonNetwork.Disconnect();
        }
        else
        {
            Debug.Log("Not connected to Photon.");
        }
    }

    // Callback huoneesta poistumisen jälkeen (valinnainen)
    public override void OnLeftRoom()
    {
        Debug.Log("Successfully left the room.");
    }

    // Callback yhteyden katkaisemisen jälkeen (valinnainen)
    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.Log($"Disconnected from Photon. Cause: {cause}");
    }
}
