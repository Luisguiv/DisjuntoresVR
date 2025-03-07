using Photon.Pun;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class PlayerSetup : MonoBehaviourPun
{
    public XROrigin xrOrigin;
    public CharacterController characterController;
    public InputActionManager inputActionManager;
    public XRInputModalityManager xrInputModalityManager;

    public GameObject playerCamera;

    public ControllerInputActionManager leftControllerInputActionManager;
    public XRInteractionGroup leftXRInteractionGroup;
    public HapticImpulsePlayer leftHapticImpulsePlayer;
    public TrackedPoseDriver leftTrackedPoseDriver;

    public GameObject leftPokeInteractor;
    public GameObject leftNearFarInteractor;
    public GameObject leftTeleportInteractor;
    public ControllerAnimator leftControllerVisual;
    public GameObject leftControllerTeleportStabilizedOrigin;

    public ControllerInputActionManager rightControllerInputActionManager;
    public XRInteractionGroup rightXRInteractionGroup;
    public HapticImpulsePlayer rightHapticImpulsePlayer;
    public TrackedPoseDriver rightTrackedPoseDriver;

    public GameObject rightPokeInteractor;
    public GameObject rightNearFarInteractor;
    public GameObject rightTeleportInteractor;
    public ControllerAnimator rightControllerVisual;
    public GameObject rightControllerTeleportStabilizedOrigin;

    public GameObject playerLocomotion;

    public Renderer helmetRenderer;

    void Start()
    {
        if (!photonView.IsMine)
        {
            // Desativa câmera e controle para outros jogadores
            //playerCamera.SetActive(false);
            //leftController.SetActive(false);
            //rightController.SetActive(false);
            return;
        }

        IsLocalPlayer();
    }

    /*public void InitializeControllers()
    {
        if (photonView.IsMine)
        {
            leftController.SetActive(true);
            rightController.SetActive(true);
        }
        else
        {
            leftController.SetActive(false);
            rightController.SetActive(false);
        }
    }*/

    public void IsLocalPlayer()
    {
        xrOrigin.enabled = true;
        characterController.enabled = true;
        inputActionManager.enabled = true;
        xrInputModalityManager.enabled = true;

        playerCamera.SetActive(true);

        leftControllerInputActionManager.enabled = true;
        leftXRInteractionGroup.enabled = true;
        leftHapticImpulsePlayer.enabled = true;
        leftTrackedPoseDriver.enabled = true;

        leftPokeInteractor.SetActive(true);
        leftNearFarInteractor.SetActive(true);
        leftTeleportInteractor.SetActive(true);
        leftControllerVisual.enabled = true;
        leftControllerTeleportStabilizedOrigin.SetActive(true);

        rightControllerInputActionManager.enabled = true;
        rightXRInteractionGroup.enabled = true;
        rightHapticImpulsePlayer.enabled = true;
        rightTrackedPoseDriver.enabled = true;

        rightPokeInteractor.SetActive(true);
        rightNearFarInteractor.SetActive(true);
        rightTeleportInteractor.SetActive(true);
        rightControllerVisual.enabled = true;
        rightControllerTeleportStabilizedOrigin.SetActive(true);

        playerLocomotion.SetActive(true);
    }

    public void SetHelmetColor(Material helmetMaterial)
    {
        if (helmetRenderer != null)
        {
            helmetRenderer.material = helmetMaterial;
        }
    }
}
