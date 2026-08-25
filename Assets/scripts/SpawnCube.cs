using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpawnCube : NetworkBehaviour
{
    [SerializeField]
    GameObject cube;

    InputSystem_Actions inputSystemActions;
    InputAction spawnCaixa;

    private void Awake()
    {
        inputSystemActions = new InputSystem_Actions();
        spawnCaixa = inputSystemActions.Player.Interact;
    }

    private void OnEnable()
    {
        spawnCaixa.Enable();
    }

    private void OnDisable()
    {
        spawnCaixa.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if(spawnCaixa.WasPressedThisFrame())
        {
            InserirCaixaCenaServerRPC();
            
        }
    }

    [Rpc(SendTo.Server)]
    public void InserirCaixaCenaServerRPC()
    {
       GameObject caixaSandra = Instantiate(cube, transform.position + transform.forward * 2, transform.rotation);

        NetworkObject instCaixaSandra = caixaSandra.GetComponent<NetworkObject>();

        instCaixaSandra.Spawn();
        
        Destroy(caixaSandra, 5.0f);
    }

    
}
