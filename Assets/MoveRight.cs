using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public Animator animator;

    public void OnClick()
    {
        animator.Play("MoveTranspalet");
    }
}
