using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dialogueInteractable : MonoBehaviour
{
    [SerializeField] 
    private List<string> dialogueFiles;

    public GameObject outlinedSprite;

    private int currentIndex = 0;

    public bool disableSpriteOnDialogueStart = true;

    public CanvasGroup canvasGroup;

    void Start() {
        canvasGroup = GetComponent<CanvasGroup>();

        MenuManager.Instance.openDialogueMenu += disableSprite;
        MenuManager.Instance.closeDialogueMenu += enableSprite;
    }

    public void OnPointerClick()
    {
        Debug.Log(dialogueFiles[currentIndex]);
        DialogueManager.Instance?.TriggerDialogue(dialogueFiles[currentIndex]);
        currentIndex = Mathf.Min(currentIndex + 1, dialogueFiles.Count - 1);
    }

    public void OnPointerEnter() {
        outlinedSprite.SetActive(true);
    }

    public void OnPointerExit() {
        outlinedSprite.SetActive(false);
    }

    public List<string> GetAllDialogues() {
        return dialogueFiles;
    }

    public void disableSprite() {
        if (disableSpriteOnDialogueStart) {
            canvasGroup.alpha = 0f;
        }
    }

    public void enableSprite() {
        canvasGroup.alpha = 1f;
    }
}
