using UnityEngine;
using Unity.Cinemachine;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//          Box Trigger Collider !          //
//                                          //
//////////////////////////////////////////////

public class CamTriggerz : MonoBehaviour
{
    [Header("Camera to activate while player is inside zones")]
    [SerializeField] private CinemachineCamera zoneCamera;
    [SerializeField] private int activePriority = 20;
    [SerializeField] private int inactivePriority = 0;

    [Header("Movement mode inside this zone")]
    [SerializeField] private bool enableOpenMovement = false;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMoveIt>();
        if (player == null) return;

        if (zoneCamera != null) zoneCamera.Priority = activePriority;
        player.SetOpenMovement(enableOpenMovement);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMoveIt>();
        if (player == null) return;

        if (zoneCamera != null) zoneCamera.Priority = inactivePriority;
        player.SetOpenMovement(false);      // goes back to the constrained movement by default :3
    }
}
