using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform spawnPoint;

    public Material tutorHelmetMaterial;  // Material do capacete do Tutor
    public Material traineeHelmetMaterial; // Material do capacete do Trainee

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            // Instanciar o jogador na posi��o inicial
            GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);

            // Verificar a fun��o do jogador e mudar a cor do capacete
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Role"))
            {
                string role = (string)PhotonNetwork.LocalPlayer.CustomProperties["Role"];
                PlayerSetup playerSetup = player.GetComponent<PlayerSetup>();

                if (role == "Tutor")
                {
                    playerSetup.SetHelmetColor(tutorHelmetMaterial);
                }
                else
                {
                    playerSetup.SetHelmetColor(traineeHelmetMaterial);
                }
            }
        }
    }
}