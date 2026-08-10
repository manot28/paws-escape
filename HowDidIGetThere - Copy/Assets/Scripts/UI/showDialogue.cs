using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private List<string> dialogueLines;

    private bool triggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            dialogueManager.ShowDialogue(dialogueLines);
        }
    }
}