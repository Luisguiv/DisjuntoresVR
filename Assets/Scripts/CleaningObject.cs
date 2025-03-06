using UnityEngine;
using Photon.Pun;

public class CleaningObject : MonoBehaviourPun
{
    public Material dirtyMaterial; // Material inicial (sujo)
    public Material cleanMaterial; // Material ap�s limpeza
    public string cleaningTag = "Cleaner"; // Tag do objeto de limpeza
    private MeshRenderer meshRenderer;
    private bool isClean = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = dirtyMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(cleaningTag) && !isClean)
        {
            if (PhotonNetwork.IsConnected)
            {
                photonView.RPC("StartCleaning", RpcTarget.AllBuffered);
            }
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(cleaningTag) && !isClean)
        {
            float speed = other.GetComponent<Rigidbody>().linearVelocity.magnitude;

            if (speed > 0.2f) // Ajuste o valor conforme necess�rio para detec��o de esfregamento
            {
                Debug.Log("Esfregando...");
                StartCleaning();
            }
        }
    }*/

    [PunRPC]
    private void StartCleaning()
    {
        if (isClean) return; // Evita chamadas desnecess�rias
        isClean = true;
        meshRenderer.material = cleanMaterial;
        Debug.Log("Objeto limpo! Textura sincronizada para todos.");
    }
}