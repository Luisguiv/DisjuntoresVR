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

    public float moveSpeed = 1f; // Velocidade de movimento

    public GameObject to_hide;
    public GameObject to_show;

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
            objectsToMoveUp[2].GetComponent<SyncActivation>().SetActiveState(true);
            MoveUp();
        }

        if (stage == 3)
        {
            to_hide.GetComponent<SyncActivation>().SetActiveState(false);
            to_show.GetComponent<SyncActivation>().SetActiveState(true);

            piece_1.GetComponent<Rigidbody>().isKinematic = false;
            piece_1.GetComponent<Rigidbody>().useGravity = true;
            piece_2.GetComponent<Rigidbody>().isKinematic = false;
            piece_2.GetComponent<Rigidbody>().useGravity = true;

            strap_animator.Play("PutStrap");
            pieces_animator.Play("RemovePieces");

            stage = 4;
        }

        if(stage == 6)
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
                float newZ = Mathf.MoveTowards(objectsToMoveSideways[i].transform.position.z, targetZValues[i], moveSpeed * Time.deltaTime);
                objectsToMoveSideways[i].transform.position = new Vector3(objectsToMoveSideways[i].transform.position.x, objectsToMoveSideways[i].transform.position.y, newZ);

                allSidewaysReached = false;
            }
        }

        if (allSidewaysReached) // Apenas inicia MoveDownObj uma vez
        {
            objectsToMoveUp[2].GetComponent<SyncActivation>().SetActiveState(false);

            stage = 3;
        }
    }
}