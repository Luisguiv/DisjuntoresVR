using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingEndManager : MonoBehaviourPun
{
    public GameObject trainingCompletePanel;

    // Chamada quando o treinamento termina
    public void ShowCompletionPanel()
    {
        trainingCompletePanel.GetComponent<SyncActivation>().ToggleChildrenObjects(true);
    }

    // Repetir o treinamento (recarrega a cena atual)
    public void RestartTraining()
    {
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
    }

    // Voltar para o Lobby (Apenas o MasterClient pode chamar)
    public void ReturnToLobby()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GIS");
        }
    }
}