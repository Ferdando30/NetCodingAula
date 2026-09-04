using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetCrystal : NetworkBehaviour
{
    private int Pontos;

    private GameObject cristalAtual;

    InputSystem_Actions inputSystemActions;
    InputAction destroyCrystal;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        destroyCrystal = inputSystemActions.Player.PickUpCrystal;

        Pontos = 0;
        string nomePlayer = $"Player {OwnerClientId + 1}";
        print($"{nomePlayer}: {Pontos}");
    }

    private void OnEnable()
    {
        destroyCrystal.Enable();
    }

    private void OnDisable()
    {
        destroyCrystal.Disable();
    }

   

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (cristalAtual != null && destroyCrystal.WasPressedThisFrame())
        {
            NetworkObject networkObjectCristal =
                cristalAtual.GetComponent<NetworkObject>();

            if (networkObjectCristal != null)
            {
                PegarCristalRpc(
                    new NetworkObjectReference(networkObjectCristal)
                );
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cristal"))
            cristalAtual = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == cristalAtual)
            cristalAtual = null;
    }

    [Rpc(SendTo.Server)]
    public void PegarCristalRpc(NetworkObjectReference cristalReference)
    {
        if (!cristalReference.TryGet(out NetworkObject cristal))
            return;

        cristal.Despawn(true);

        Pontos++;
        string nomePlayer = $"Player {OwnerClientId + 1}";
        print($"{nomePlayer}: {Pontos}");
    }
}
