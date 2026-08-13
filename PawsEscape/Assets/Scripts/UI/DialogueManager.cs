using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.03f;

    private List<string> currentLines;
    private int currentLineIndex;

    private bool isTyping;
    public bool dialogueActive;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void ShowDialogue(List<string> lines)
    {
        if (dialogueActive) return; // prevents reopening

        dialoguePanel.SetActive(true);
        playerController.isListening = true;
        currentLines = lines;
        currentLineIndex = 0;
        dialogueActive = true;
        StartCoroutine(TypeLine());
    }

    private void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentLines[currentLineIndex];
                isTyping = false;
            }
            else
                NextLine();
        }
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in currentLines[currentLineIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void NextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= currentLines.Count)
        {
            HideDialogue();
            return;
        }

        StartCoroutine(TypeLine());
    }

    public void HideDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        dialogueActive = false;
        playerController.isListening = false;
    }
}