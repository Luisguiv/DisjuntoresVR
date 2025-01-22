using System.Collections;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private float targetY = .53f; // Posição mínima do eixo Y
    public float moveSpeed; // Velocidade do movimento
    private Coroutine moveCoroutine = null; // Referência para a corrotina ativa

    public StageManager stageManager;

    public void StartMoveDown()
    {
        // Apenas inicia a corrotina se não houver uma em andamento
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
            moveCoroutine = null; // Reseta a referência
        }
    }

    private IEnumerator MoveDownCoroutine()
    {
        while (transform.position.y > targetY)
        {
            // Calcula a nova posição de Y de forma suave
            float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            yield return null; // Aguarda o próximo frame
        }
        if (transform.position.y == targetY)
        {
            stageManager.SetStage(2);
        }

        moveCoroutine = null; // Reseta a referência quando o movimento termina
    }
}