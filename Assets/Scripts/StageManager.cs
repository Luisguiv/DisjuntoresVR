using Unity.XR.CoreUtils;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StageManager : MonoBehaviour
{
    public int stage;

    public GameObject[] objectsToMoveUp; // Array de objetos a serem movidos para cima
    public float[] targetYValues;

    public GameObject[] objectsToMoveSideways;
    public float[] targetZValues;

    public GameObject[] objectsToMoveDown;
    public float[] targetYDownValues;

    public float moveSpeed = 1f; // Velocidade de movimento

    public GameObject to_hide;
    public GameObject to_show;

    public Animator strap_animator;
    public Animator pieces_animator;

    void Start()
    {
        stage = 0;
    }

    private void Update()
    {
        if (stage == 2)
        {
            objectsToMoveUp[2].SetActive(true);
            MoveUp();
        }

        if (stage == 3)
        {
            to_hide.SetActive(false);
            to_show.SetActive(true);

            strap_animator.Play("PutStrap");
            pieces_animator.Play("RemovePieces");

            stage = 4;
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

    private void MoveUp()
    {
        bool allReachedTarget = true;

        for (int i = 0; i < objectsToMoveUp.Length; i++)
        {
            if (objectsToMoveUp[i].transform.position.y < targetYValues[i])
            {
                // Move cada objeto para sua altura final
                float newY = Mathf.MoveTowards(objectsToMoveUp[i].transform.position.y, targetYValues[i], moveSpeed * Time.deltaTime);
                objectsToMoveUp[i].transform.position = new Vector3(objectsToMoveUp[i].transform.position.x, newY, objectsToMoveUp[i].transform.position.z);

                allReachedTarget = false; // Ainda há objetos se movendo para cima
            }
        }

        // Quando todos os objetos atingirem suas respectivas alturas, inicia o movimento lateral
        if (allReachedTarget)
        {
            MoveSideways();
        }
    }

    private void MoveSideways()
    {
        bool allSidewaysReached = true;

        for (int i = 0; i < objectsToMoveSideways.Length; i++)
        {
            if (objectsToMoveSideways[i].transform.position.z < targetZValues[i])
            {
                // Move cada objeto para sua posição final no eixo Z
                float newZ = Mathf.MoveTowards(objectsToMoveSideways[i].transform.position.z, targetZValues[i], moveSpeed * Time.deltaTime);
                objectsToMoveSideways[i].transform.position = new Vector3(objectsToMoveSideways[i].transform.position.x, objectsToMoveSideways[i].transform.position.y, newZ);

                allSidewaysReached = false; // Ainda há objetos se movendo lateralmente
            }
        }

        // Verificação final (caso precise adicionar alguma ação depois que todos os objetos se moverem no eixo Z)
        if (allSidewaysReached)
        {
            MoveDownObj();
        }
    }

    private void MoveDownObj()
    {
        for (int i = 0; i < objectsToMoveDown.Length; i++)
        {
            if (objectsToMoveDown[i].transform.position.y > targetYDownValues[i])
            {
                // Move cada objeto para sua altura final para baixo
                float newY = Mathf.MoveTowards(objectsToMoveDown[i].transform.position.y, targetYDownValues[i], moveSpeed * Time.deltaTime);
                objectsToMoveDown[i].transform.position = new Vector3(objectsToMoveDown[i].transform.position.x, newY, objectsToMoveDown[i].transform.position.z);
            }
        }

        stage = 3;
    }
}