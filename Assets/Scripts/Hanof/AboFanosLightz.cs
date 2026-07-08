using UnityEngine;

public class AboFanosLightz : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private GameObject lightsOff;           // the light we want to turn off ! :3
    [SerializeField] private GameObject lightsOn;             // the light we want to turn on
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        lightsOff.SetActive(false);
        lightsOn.SetActive(true);
    }
}
