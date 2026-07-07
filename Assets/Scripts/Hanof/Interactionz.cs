using UnityEngine;
using UnityEngine.InputSystem;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//          Box Trigger Collider !          //
//                                          //
//////////////////////////////////////////////

// script's purpose: Enable the player to interact with stuff using box collider not raytracing !

// script's requirements: Box Collider with IsTrigger activated, prompt UI, and input system !
public class Interactionz : MonoBehaviour
{
    [Header("Prompt")]
    [SerializeField] private GameObject promptUI;           // the prompt we want to show the player ! :3
    [SerializeField] private GameObject hintPrompt;         // the prompt guiding the player on what to do

    [Header("Input")]
    [SerializeField] private InputActionReference interactAction;

    [Header("On Interact")]
    [SerializeField] private AudioClip interactSound;       // the audio we want to play ! 
    [SerializeField] private GameObject vfxPrefab;          // any vfx we wanna add
    [SerializeField] private GameObject imAlive;   // the object we want to activate on interact
    [SerializeField] private GameObject imDead; // the object we want to DESTROYYYY - or u can just parent it to the trigger

    private bool playerInRange = false;                     // this is to show the prompt or hide it

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = true;
        if (promptUI != null) promptUI.SetActive(true);

        if (interactAction != null)
        {
            interactAction.action.performed += OnInteractPerformed;
            interactAction.action.Enable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
        if (promptUI != null) promptUI.SetActive(false);

        if (interactAction != null)
        {
            interactAction.action.performed -= OnInteractPerformed;
        }
    }

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        if (!playerInRange) return;
        DoInteract();
    }

    private void DoInteract()
    {
        if (interactAction != null)
            interactAction.action.performed -= OnInteractPerformed;

        if (interactSound != null)
            AudioSource.PlayClipAtPoint(interactSound, transform.position);

        if (vfxPrefab != null)
            Instantiate(vfxPrefab, transform.position, Quaternion.identity);

        if (imAlive != null)
            imAlive.SetActive(true);

        if (imDead != null)
            imDead.SetActive(false);

        if (promptUI != null) promptUI.SetActive(false);

        if (hintPrompt != null) hintPrompt.SetActive(true);     // make sure the hint prompt object has a timer on it ! :3 (die script)

        Destroy(gameObject);
    }
}
