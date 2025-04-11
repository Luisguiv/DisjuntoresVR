using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.XR;
public class RadialSelection : MonoBehaviour
{

   // Usando XR Input para capturar a entrada do controlador
    public XRNode inputSource = XRNode.RightHand;  // Ou LeftHand, dependendo de qual controlador você deseja monitorar
    private InputDevice device;

    // public OVRInput.Button spawnBotton;

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
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void Update()
    {
        // // Verifica se o botão de spawn foi pressionado
        // if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
        // {
        //     SpawnRadialPart();
        // }

        // // Verifica se o botão primário ainda está pressionado
        // if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isPressed) && isPressed)
        // {
        //     GetSelectedRadialPart();
        // }

        // // Verifica se o botão foi solto
        // if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonReleased) && !buttonReleased)
        // {
        //     HideAndTriggerSelected();
        // }

        SpawnRadialPart();
        GetSelectedRadialPart();
        //HideAndTriggerSelected();
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

        float angle = Vector3.SignedAngle(radialPartCanvas.up, centerToHandProject, -radialPartCanvas.forward);

        if (angle < 0)
            angle += 360;

        // Ajusta para garantir que o valor de angle seja usado corretamente
        currentSelectedRadialPart = Mathf.FloorToInt(angle * numerOfRadialPart / 360);

        Debug.Log("Angle: " + angle);
        Debug.Log("Selected Radial Part Index: " + currentSelectedRadialPart);

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
        // radialPartCanvas.gameObject.SetActive(true);
        // radialPartCanvas.position = handTransform.position;
        // radialPartCanvas.rotation = handTransform.rotation;

        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }

        spawnedParts.Clear();

        for(int i = 0; i < numerOfRadialPart; i++)
        {
            float angle = - i * 360 / numerOfRadialPart - angleBetweenPart/2;
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
