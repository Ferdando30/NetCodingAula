using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetCrystal : MonoBehaviour
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
        print(Pontos);
    }

    private void OnEnable()
    {
        destroyCrystal.Enable();
    }

    private void OnDisable()
    {
        destroyCrystal.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cristalAtual != null && destroyCrystal.WasPressedThisFrame())
        {
            PegarCristal();
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
    public void PegarCristal()
    {
        Destroy(cristalAtual);
        cristalAtual = null;

        Pontos++;
        print(Pontos);
    }
}
