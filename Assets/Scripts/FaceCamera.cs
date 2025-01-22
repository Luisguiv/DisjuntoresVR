using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Transform cameraTransform; // Refer�ncia ao transform da c�mera

    void Update()
    {
        // Procura a c�mera principal na cena
        while (GameObject.FindWithTag("MainCamera") == null)
        {
            Debug.Log("Nao achou camera");
        }

        Debug.Log("Achou");
        cameraTransform = GameObject.FindWithTag("MainCamera").transform;

        // Faz o objeto olhar para a c�mera
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // Ajuste a rota��o se necess�rio
    }
}
