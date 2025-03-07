using UnityEngine;
using Photon.Pun;

public class MoveDown : MonoBehaviourPun
{
    private float targetY = .62f; // Posição mínima do eixo Y
    public float moveSpeed; // Velocidade do movimento
    private bool isMoving = false; // Controle de movimento

    public StageManager stageManager;

    void Update()
    {
        if (isMoving)
        {
            float newY = Mathf.Lerp(transform.position.y, targetY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // Sincroniza apenas quando está perto do destino
            if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
            {
                isMoving = false;
                photonView.RPC("RPC_SyncPosition", RpcTarget.AllBuffered, transform.position);
                stageManager.SetStage(2);
            }
        }
    }

    public void StartMoveDown()
    {
        if(stageManager.stage == 1)
        {
            // Solicita a propriedade do objeto antes de iniciar o movimento
            if (!photonView.IsMine)
            {
                photonView.RequestOwnership();
            }

            // Agora qualquer jogador pode chamar este RPC
            photonView.RPC("RPC_StartMoveDown", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_StartMoveDown()
    {
        isMoving = true;
    }

    [PunRPC]
    void RPC_SyncPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
