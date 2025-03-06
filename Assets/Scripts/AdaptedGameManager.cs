using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class AdaptedGameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            // Instanciar o jogador na posição inicial
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
            PlayerSetup playerSetup = player.GetComponent<PlayerSetup>();
        }
    }
}