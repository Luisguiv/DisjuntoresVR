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
            rb.isKinematic = false; // Permitir f�sica para o propriet�rio
        }
        else
        {
            rb.isKinematic = true; // Desativar f�sica para outros jogadores
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
