using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class SceneStartManager : MonoBehaviourPunCallbacks
{
    public GameObject gameplayElements; // Pelielementit, jotka aktivoidaan

    void Update()
    {
        // Tarkistetaan, onko huoneessa 2 pelaajaa
        if (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            gameplayElements.SetActive(true); // Aktivoi pelielementit
            this.enabled = false; // Lopettaa t�m�n skriptin p�ivityksen, koska se on nyt tarpeeton
        }
    }
}