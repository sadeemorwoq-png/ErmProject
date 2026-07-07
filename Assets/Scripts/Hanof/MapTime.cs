using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

//////////////////////////////////////////////  
//                                          //
//       This script shall live on:         //
//              The Player !                //
//                                          //
//////////////////////////////////////////////

// script's purpose: enables the player to open and close a map !

// script's requirements: UI(Canvas) map set up :)

// important to note: uses new input :3
public class MapTime : MonoBehaviour
{
    [SerializeField] private GameObject mapCanvas;

    // audio options for effects
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private bool isMapOpen = false;

    private void Start()
    {
        if (mapCanvas != null) mapCanvas.SetActive(false);
    }
    public void OnMap(InputValue value)
    {
        if (!value.isPressed) return;

        isMapOpen = !isMapOpen;

        if (mapCanvas != null) mapCanvas.SetActive(isMapOpen);
        Time.timeScale = isMapOpen ? 0f : 1f;

        PlayToggleSound();
    }
    private void PlayToggleSound()
    {
        if (audioSource == null) return;

        AudioClip clipToPlay = isMapOpen ? openSound : closeSound;
        if (clipToPlay != null)
            audioSource.PlayOneShot(clipToPlay);
    }
}
