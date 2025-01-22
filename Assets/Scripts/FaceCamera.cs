using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform cameraTransform; // Referência ao transform da câmera

    void Update()
    {
        // Procura a câmera principal na cena
        while (GameObject.FindWithTag("MainCamera") == null)
        {
            Debug.Log("Nao achou camera");
        }

        Debug.Log("Achou");
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;

        // Faz o objeto olhar para a câmera
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // Ajuste a rotação se necessário
    }
}
