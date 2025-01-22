using System.Collections;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float targetY = .53f; // Posi��o m�nima do eixo Y
    public float moveSpeed; // Velocidade do movimento
    private Coroutine moveCoroutine = null; // Refer�ncia para a corrotina ativa

    public StageManager stageManager;

    public void StartMoveDown()
    {
        // Apenas inicia a corrotina se n�o houver uma em andamento
        if (stageManager.GetStage() == 1 && transform.position.y != targetY && moveCoroutine == null)
        {
            //Debug.Log(transform.position.y);
            moveCoroutine = StartCoroutine(MoveDownCoroutine());
        }
    }

    public void StopMoveDown()
    {
        // Para a corrotina em andamento, se existir
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null; // Reseta a refer�ncia
        }
    }

    private IEnumerator MoveDownCoroutine()
    {
        while (transform.position.y > targetY)
        {
            // Calcula a nova posi��o de Y de forma suave
            float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null; // Aguarda o pr�ximo frame
        }
        if (transform.position.y == targetY)
        {
            stageManager.SetStage(2);
        }

        moveCoroutine = null; // Reseta a refer�ncia quando o movimento termina
    }
}