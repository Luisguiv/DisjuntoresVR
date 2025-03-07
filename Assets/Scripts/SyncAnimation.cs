using Photon.Pun;
using UnityEngine;

public class SyncAnimation : MonoBehaviourPun
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // M�todo para iniciar a anima��o, chamando um RPC
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

    // RPC que ser� executado por todos os jogadores
    [PunRPC]
    void SyncAnimationRPC(string animationTrigger)
    {
        animator.Play(animationTrigger);
    }
}