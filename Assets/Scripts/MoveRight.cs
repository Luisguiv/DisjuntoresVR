using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public Animator animator;
    public GameObject transpalet;

    public void OnClick()
    {
        transpalet.GetComponent<SyncAnimation>().PlayAnimation("MoveTranspalet");
    }
}
