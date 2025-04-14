using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR;
public class RadialSelection : MonoBehaviour
{

    // Usando XR Input para capturar a entrada do controlador
    // public XRNode leftInputSource = XRNode.LeftHand; // Mão esquerda
    // public XRNode rightInputSource = XRNode.RightHand; // Mão direita
    // private InputDevice leftDevice;
    // private InputDevice rightDevice;

    public XRNode inputSource = XRNode.RightHand;  // Ou LeftHand
    private InputDevice device;

    [Range(2,10)]
    public int numerOfRadialPart;
    public GameObject radialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenPart = 10;
    public Transform handTransform;


    public UnityEvent<int> OnPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;
    void Start()
    {
        // Inicializa o dispositivo do controlador no início
        // leftDevice = InputDevices.GetDeviceAtXRNode(leftInputSource);
        // rightDevice = InputDevices.GetDeviceAtXRNode(rightInputSource);

        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se o botão da mão esquerda foi pressionado para abrir o menu
        // if (leftDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonPressed) && leftPrimaryButtonPressed)
        // {
        //     Debug.Log("Left Primary Button Pressed!");
        //     SpawnRadialPart(); // Mostra o Pie Menu
        // }

        // // Verifica se o botão da mão direita está pressionado para selecionar uma parte
        // if (rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonPressed) && rightPrimaryButtonPressed)
        // {
        //     GetSelectedRadialPart(); // Seleciona a parte do Pie Menu
        // }

        // // Verifica se o botão foi solto para esconder o menu e disparar o evento de seleção
        // if (rightDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightButtonReleased) && !rightButtonReleased)
        // {
        //     HideAndTriggerSelected(); // Esconde o menu e dispara a seleção
        // }

        //SpawnRadialPart();
        //GetSelectedRadialPart();
        //HideAndTriggerSelected();

        if (device.isValid)
        {
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
            {
                Debug.Log("Primary Button Pressed!");
            }
        }
        else
        {
            Debug.Log("Device is not valid!");
        }
    }

    public void HideAndTriggerSelected()
    {
        OnPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }
    public void GetSelectedRadialPart()
    {
        Vector3 centerToHand = handTransform.position - radialPartCanvas.position;
        Vector3 centerToHandProject = Vector3.ProjectOnPlane(centerToHand, radialPartCanvas.forward);

        // Calcule o ângulo entre a direção do Canvas e a direção da mão
        float angle = Vector3.SignedAngle(radialPartCanvas.up, -centerToHandProject, radialPartCanvas.forward);

        // Inverta o ângulo para corrigir o problema da seleção invertida
        if (angle < 0)
            angle += 360;

        angle = 360 - angle;

        // Ajuste o cálculo do índice de seleção radial
        currentSelectedRadialPart = Mathf.FloorToInt(angle * numerOfRadialPart / 360);

        Debug.Log("Angle: " + angle);
        Debug.Log("Selected Radial Part Index: " + currentSelectedRadialPart);

        // Atualize as partes selecionadas
        for (int i = 0; i < numerOfRadialPart; i++)
        {
            if (i == currentSelectedRadialPart)
            {
                spawnedParts[i].GetComponent<Image>().color = Color.yellow;
                spawnedParts[i].transform.localScale = 1.1f * Vector3.one;
            }
            else
            {
                spawnedParts[i].GetComponent<Image>().color = Color.white;
                spawnedParts[i].transform.localScale = 1f * Vector3.one;
            }
        }
    }


    public void SpawnRadialPart()
    {
        radialPartCanvas.gameObject.SetActive(true);
        radialPartCanvas.position = handTransform.position;
        radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for(int i = 0; i < numerOfRadialPart; i++)
        {
            float angle =  - i * 360 / numerOfRadialPart - angleBetweenPart/2;
            Vector3 radialPartEulerAngle = new Vector3(0, 0, angle);

            GameObject spawnedRadialPart = Instantiate(radialPartPrefab, radialPartCanvas);
            spawnedRadialPart.transform.position = radialPartCanvas.position;
            spawnedRadialPart.transform.localEulerAngles = radialPartEulerAngle;

            // Defina o tipo de imagem como Filled e o método como Radial360
            Image image = spawnedRadialPart.GetComponent<Image>();
            image.type = Image.Type.Filled;
            image.fillMethod = Image.FillMethod.Radial360;

            float calculatedFillAmount = (1 / (float)numerOfRadialPart) - (angleBetweenPart / 360);
            image.fillAmount = calculatedFillAmount;

            spawnedParts.Add(spawnedRadialPart);
        }
    }
}
