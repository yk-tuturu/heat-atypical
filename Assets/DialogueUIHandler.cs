using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUIHandler : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI speakerText;
    [SerializeField] 
    private TextMeshProUGUI speechText;
    [SerializeField] 
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private DialogueSpriteManager spriteManager;

    public bool isTyping = false;
    public float textDelay = 0.02f;
    private Dialogue currentLine;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    public void ShowDialoguePanel() {
        dialoguePanel.SetActive(true);
    }

    public void HideDialoguePanel() {
        dialoguePanel.SetActive(false);
    }

    public void DisplayNextDialogue(Dialogue dialogue) {
        currentLine = dialogue;
        speakerText.text = dialogue.speaker;
        spriteManager.ShowSprite(dialogue.speaker);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.sentence));
        nextButton.SetActive(false);
    }

    public void SkipTextAnimation() {
        if (!isTyping) return;

        StopAllCoroutines();
        speechText.text = currentLine.sentence;
        EndTyping();
    }

    public void NextButtonClicked() {
        DialogueManager.Instance?.FetchNextDialogue();
    }

    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        speechText.text = "";
        isTyping = true;
        foreach(char letter in sentence.ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
        EndTyping();
    }

    public void EndTyping() {
        isTyping = false;
        speechText.text = currentLine.sentence;
        nextButton.SetActive(true);
    }

    public void EndDialogue() {
        isTyping = false;
        speechText.text = "";
        speakerText.text = "";
        HideDialoguePanel();
        spriteManager.HideAllSprites();
    }
}
