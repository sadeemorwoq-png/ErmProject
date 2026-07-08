using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;
    public TMP_Text continueText;

    [HideInInspector] public bool isDialogueActive = false;

    private string[] speakers;
    private string[] lines;
    private int index = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void StartDialogue(string[] speakerNames, string[] dialogueLines)
    {
        speakers = speakerNames;
        lines = dialogueLines;
        index = 0;

        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        ShowCurrentLine();
    }

    void NextLine()
    {
        index++;

        if (index >= lines.Length)
        {
            dialoguePanel.SetActive(false);
            isDialogueActive = false;
            return;
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        speakerText.text = speakers[index];
        dialogueText.text = lines[index];
        continueText.text = "Press E to Continue";
    }
}