using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject pressEText;

    private bool playerInside = false;
    private bool dialogueStarted = false;

    private string[] speakers =
    {
        "Abu Fanous",
        "Jude",
        "Abu Fanous",
        "Jude",
        "Abu Fanous",
        "Jude",
        "Abu Fanous"
    };

    private string[] dialogue =
    {
        "Welcome, traveler. I have been waiting for you.",
        "Who are you?",
        "My name is Abu Fanous. I know the only way out of the City of Erm.",
        "Then help me escape.",
        "It will not be easy. You must prove yourself first.",
        "I am ready.",
        "Follow me. Our journey begins now."
    };

    void Start()
    {
        pressEText.SetActive(false);
    }

    void Update()
    {
        if (playerInside && !dialogueStarted && Input.GetKeyDown(KeyCode.E))
        {
            dialogueStarted = true;
            pressEText.SetActive(false);

            dialogueManager.StartDialogue(speakers, dialogue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (!dialogueStarted)
                pressEText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            pressEText.SetActive(false);
        }
    }
}