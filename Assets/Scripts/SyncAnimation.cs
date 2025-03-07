using Photon.Pun;
using UnityEngine;

public class SyncAnimation : MonoBehaviourPun
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Método para iniciar a animação, chamando um RPC
    public void PlayAnimation(string animationTrigger)
    {
        if (PhotonNetwork.IsConnected)
        {
            photonView.RPC("SyncAnimationRPC", RpcTarget.AllBuffered, animationTrigger);
        }
        else
        {
            SyncAnimationRPC(animationTrigger);
        }
    }

    // RPC que será executado por todos os jogadores
    [PunRPC]
    void SyncAnimationRPC(string animationTrigger)
    {
        animator.Play(animationTrigger);
    }
}