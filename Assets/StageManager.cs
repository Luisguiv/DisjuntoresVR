using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage;

    void Start()
    {
        stage = 0;
    }

    public void SetStage(int current)
    {
        stage = current;
    }

    public int GetStage()
    {
        return stage;
    }
}
