using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private string dialogueFile;

    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueManager.Instance?.TriggerDialogue(dialogueFile);
    }
}
