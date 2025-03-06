using UnityEngine;

public class CleaningObject : MonoBehaviour
{
    public Material dirtyMaterial; // Material inicial (sujo)
    public Material cleanMaterial; // Material após limpeza
    public string cleaningTag = "Cleaner"; // Tag do objeto de limpeza
    private MeshRenderer meshRenderer;
    private bool isClean = false;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = dirtyMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(cleaningTag) && !isClean)
        {
            StartCleaning();
        }
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(cleaningTag) && !isClean)
        {
            float speed = other.GetComponent<Rigidbody>().linearVelocity.magnitude;

            if (speed > 0.2f) // Ajuste o valor conforme necessário para detecção de esfregamento
            {
                Debug.Log("Esfregando...");
                StartCleaning();
            }
        }
    }*/

    private void StartCleaning()
    {
        isClean = true;
        meshRenderer.material = cleanMaterial;
        Debug.Log("Objeto limpo!");
    }
}