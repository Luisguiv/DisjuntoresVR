using Photon.Pun;
using UnityEngine;

public class SyncActivation : MonoBehaviourPun
{
    [PunRPC]
    void SetObjectActive(bool state)
    {
        gameObject.SetActive(state);
    }

    // Alterna o estado do pr�prio objeto
    public void ToggleObject()
    {
        photonView.RPC("SetObjectActive", RpcTarget.AllBuffered, !gameObject.activeSelf);
    }

    public void PrintHello()
    {
        Debug.Log("Hello");
    }

    // M�todo p�blico para ativar/desativar manualmente o pr�prio objeto
    public void SetActiveState(bool state)
    {
        photonView.RPC("SetObjectActive", RpcTarget.AllBuffered, state);
    }

    // Ativa um objeto espec�fico passado como par�metro
    public void ActivateObject(GameObject targetObject)
    {
        if (targetObject != null)
        {
            photonView.RPC("SetObjectState", RpcTarget.AllBuffered, targetObject.name, true);
        }
    }

    // Desativa um objeto espec�fico passado como par�metro
    public void DeactivateObject(GameObject targetObject)
    {
        if (targetObject != null)
        {
            photonView.RPC("SetObjectState", RpcTarget.AllBuffered, targetObject.name, false);
        }
    }

    // PunRPC para ativar/desativar um objeto com base no nome
    [PunRPC]
    void SetObjectState(string objectName, bool state)
    {
        GameObject target = GameObject.Find(objectName);
        if (target != null)
        {
            target.SetActive(state);
        }
    }

    public void ToggleChildrenObjects(bool state)
    {
        photonView.RPC("SetChildrenObjectsActive", RpcTarget.AllBuffered, state);
    }

    [PunRPC]
    void SetChildrenObjectsActive(bool state)
    {
        gameObject.SetActive(state);

        // Ativar ou desativar todos os filhos tamb�m
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(state);
        }
    }
}