using Photon.Pun;
using UnityEngine;

public class GrabSync : MonoBehaviour
{
    private PhotonView photonView;
    private Rigidbody rb;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected && photonView.IsMine)
        {
            rb.isKinematic = false; // Permitir física para o proprietário
        }
        else
        {
            rb.isKinematic = true; // Desativar física para outros jogadores
        }
    }

    public void OnSelectEnter()
    {
        if (!photonView.IsMine)
        {
            photonView.RequestOwnership();
        }
    }
}
