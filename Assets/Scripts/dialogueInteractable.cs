using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dialogueInteractable : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] 
    private List<string> dialogueFiles;

    private int currentIndex = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueManager.Instance?.TriggerDialogue(dialogueFiles[currentIndex]);
        currentIndex = Mathf.Min(currentIndex + 1, dialogueFiles.Count - 1);
    }

    public List<string> GetAllDialogues() {
        return dialogueFiles;
    }
}
