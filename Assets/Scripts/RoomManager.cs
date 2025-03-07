using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;

    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AutomaticallySyncScene = true; // Sincroniza a cena para todos
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");

        // Criar ou entrar na sala "test"
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");

        // Criar Player na posição do SpawnPoint
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);

        // Garantir que apenas o jogador dono do objeto configure seus componentes
        if (_player.GetComponent<PhotonView>().IsMine)
        {
            _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        }
    }

    // Método para definir o papel do jogador antes de iniciar a cena
    public void SelectRole(string role)
    {
        Hashtable playerProperties = new Hashtable();
        playerProperties["Role"] = role; // "Tutor" ou "Trainee"
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

        Debug.Log("Função escolhida: " + role);
    }

    // Método para iniciar a cena do jogo (MasterClient chama)
    public void StartGame(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
        else
        {
            Debug.Log("Apenas o MasterClient pode iniciar o jogo!");
        }
    }
}