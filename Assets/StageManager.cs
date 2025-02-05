using Unity.XR.CoreUtils;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StageManager : MonoBehaviour
{
    public int stage;
    public GameObject[] objectsToMoveUp; // Array de objetos a serem movidos para cima
    public GameObject otherObject; // Objeto a ser movido no eixo Z
    public float targetY = 2f; // Altura final desejada para os objetos que sobem
    public float targetZ = 10f; // Posição final desejada para o objeto que se move no eixo Z
    public float moveSpeed = 1f; // Velocidade de movimento

    void Start()
    {
        stage = 0;
    }

    private void Update()
    {
        if (stage == 2) {
            objectsToMoveUp[2].SetActive(true);
            MoveUp();
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

        foreach (GameObject obj in objectsToMoveUp)
        {
            if (obj.transform.position.y < targetY)
            {
                // Move cada objeto para cima de forma suave
                float newY = Mathf.MoveTowards(obj.transform.position.y, targetY, moveSpeed * Time.deltaTime);
                obj.transform.position = new Vector3(obj.transform.position.x, newY, obj.transform.position.z);

                allReachedTarget = false; // Ainda há objetos se movendo
            }
        }

        // Se todos os objetos atingiram a posição alvo, inicia o movimento lateral
        if (allReachedTarget)
        {
            //MoveSideways();
        }
    }

    private void MoveSideways()
    {
        if (otherObject != null)
        {
            // Move o outro objeto no eixo Z positivamente
            float newZ = Mathf.MoveTowards(otherObject.transform.position.z, targetZ, moveSpeed * Time.deltaTime);
            otherObject.transform.position = new Vector3(otherObject.transform.position.x, otherObject.transform.position.y, newZ);
        }
    }
}
