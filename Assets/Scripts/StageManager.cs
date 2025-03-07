using UnityEngine;
using Photon.Pun;

public class StageManager : MonoBehaviourPun
{
    public int stage;

    // Agora usamos animadores em vez de GameObjects para movimentacao
    public Animator animatorToMoveUp;
    public Animator animatorToMoveSideways;

    public GameObject[] objectsToMoveUp; // Para ativação dos objetos que precisam aparecer
    public GameObject to_show;
    public GameObject to_show_2;
    public GameObject to_show_3;

    public Animator strap_animator;
    public Animator pieces_animator;
    public GameObject piece_1;
    public GameObject piece_2;

    public GameObject gameManager;

    void Start()
    {
        stage = 0;
    }

    private void Update()
    {
        if (stage == 2)
        {
            if (!photonView.IsMine)
            {
                photonView.RequestOwnership();
            }

            // Desativar Gancho e Acumulador
            objectsToMoveUp[0].GetComponent<SyncActivation>().DeactivateObject(objectsToMoveUp[0]);
            objectsToMoveUp[1].GetComponent<SyncActivation>().DeactivateObject(objectsToMoveUp[1]);

            // Ativa MoveUp
            to_show_2.SetActive(true);
            to_show_2.GetComponent<SyncActivation>().ActivateObject(to_show_2);

            photonView.RPC("RPC_PlayMoveUpAnimation", RpcTarget.AllBuffered);
        }

        if (stage == 3)
        {
            if (!photonView.IsMine)
            {
                photonView.RequestOwnership();
            }

            // Desativa MoveUp
            to_show_2.GetComponent<SyncActivation>().DeactivateObject(to_show_2);

            // Ativa MoveSideways
            to_show_3.SetActive(true);
            to_show_3.GetComponent<SyncActivation>().ActivateObject(to_show_3);

            photonView.RPC("RPC_PlayMoveSidewaysAnimation", RpcTarget.AllBuffered);
        }

        if (stage == 4)
        {
            if (!photonView.IsMine)
            {
                photonView.RequestOwnership();
            }

            // Desativa MoveSideways
            to_show_3.GetComponent<SyncActivation>().DeactivateObject(to_show_3);

            // Ativa Acumulador Animado
            to_show.SetActive(true);
            to_show.GetComponent<SyncActivation>().ActivateObject(to_show);

            photonView.RPC("RPC_PlayAnimations", RpcTarget.AllBuffered);
        }

        if (stage == 7)
        {
            gameManager.GetComponent<TrainingEndManager>().ShowCompletionPanel();
            stage++;
        }
    }

    public void SetStage(int current)
    {
        stage = current;
    }

    public int GetStage()
    {
        return stage;
    }

    // Inicia a animação de MoveUp para todos os objetos no Photon
    [PunRPC]
    private void RPC_PlayMoveUpAnimation()
    {
        animatorToMoveUp.Play("MoveUp");

        stage = 3;
    }

    // Inicia a animação de MoveSideways para todos os objetos no Photon
    [PunRPC]
    private void RPC_PlayMoveSidewaysAnimation()
    {
        animatorToMoveSideways.Play("MoveSideways");

        stage = 4;
    }

    // Sincroniza as animações no multiplayer
    [PunRPC]
    private void RPC_PlayAnimations()
    {
        piece_1.GetComponent<Rigidbody>().isKinematic = false;
        piece_1.GetComponent<Rigidbody>().useGravity = true;
        piece_2.GetComponent<Rigidbody>().isKinematic = false;
        piece_2.GetComponent<Rigidbody>().useGravity = true;

        strap_animator.Play("PutStrap");
        pieces_animator.Play("RemovePieces");

        stage = 5;
    }
}