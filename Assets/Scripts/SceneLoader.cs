using Photon.Pun;
using UnityEngine;

public class SceneLoader : MonoBehaviourPunCallbacks
{
    public void LoadScene(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient) // Somente o Host (MasterClient) pode mudar a cena
        {
            PhotonNetwork.LoadLevel(sceneName);
        }
    }
}