using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private string dialogueFile;

    public bool repeatable = false;
    private bool seen = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!repeatable && seen) return;

        DialogueManager.Instance?.TriggerDialogue(dialogueFile);
        seen = true;
    }
}
