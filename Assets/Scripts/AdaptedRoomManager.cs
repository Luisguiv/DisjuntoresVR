using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class AdaptedRoomManager : MonoBehaviourPunCallbacks
{
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