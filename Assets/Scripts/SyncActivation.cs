using Photon.Pun;
using UnityEngine;

public class SyncActivation : MonoBehaviourPun
{
    public void ToggleObject() // Alterna o estado do objeto
    {
        photonView.RPC("SetObjectActive", RpcTarget.AllBuffered, !gameObject.activeSelf);
    }

    [PunRPC]
    void SetObjectActive(bool state)
    {
        gameObject.SetActive(state);
    }

    // M�todo p�blico para ativar/desativar manualmente
    public void SetActiveState(bool state)
    {
        photonView.RPC("SetObjectActive", RpcTarget.AllBuffered, state);
    }
}