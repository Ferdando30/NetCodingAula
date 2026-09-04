using Unity.Netcode;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
namespace NetcodeDemo
{
    public class ClientPlayerMove : NetworkBehaviour
    {
        [SerializeField]
        CharacterController m_CharacterController;
        [SerializeField]
        ThirdPersonController m_ThirdPersonController;
        [SerializeField]
        PlayerInput m_PlayerInput;
        [SerializeField]
        Transform m_CameraFollow;
        private void Awake()
        {
            m_PlayerInput.enabled = false;
            m_ThirdPersonController.enabled = false;
            m_CharacterController.enabled = false;
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            enabled = IsClient; // Enable if this is a client.
            if (!IsOwner)
            {
                // Disable if this is not the owner
                enabled = false;
                m_PlayerInput.enabled = false;
                m_CharacterController.enabled = false;
                m_ThirdPersonController.enabled = false;
                return;
            }
            // Enable if this is an owner
            m_PlayerInput.enabled = true;
            m_CharacterController.enabled = true;
            m_ThirdPersonController.enabled = true;

            CinemachineCamera followCamera =
                FindFirstObjectByType<CinemachineCamera>();

            followCamera.Target.TrackingTarget =
                m_ThirdPersonController.CinemachineCameraTarget.transform;
        }
    }
}

