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
    private GameObject recPanel;

    [SerializeField]
    private DialogueSpriteManager spriteManager;

    public bool isTyping = false;
    private bool recordable = false;
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

        // set speaker text and sprite
        speakerText.text = dialogue.speaker;
        spriteManager.ShowSprite(dialogue.speaker);

        // type new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.sentence));
        
        nextButton.SetActive(false);
        recPanel.SetActive(false);

        if (dialogue is RecordableDialogue) {
            recordable = true;
        }
    }

    public void SkipTextAnimation() {
        if (!isTyping) return;

        StopAllCoroutines();
        speechText.text = currentLine.sentence;
        EndTyping();
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

        if (recordable) {
            recPanel.SetActive(true);
        } else {
            nextButton.SetActive(true);
        }
    }

    public void EndDialogue() {
        isTyping = false;
        speechText.text = "";
        speakerText.text = "";
        HideDialoguePanel();
        spriteManager.HideAllSprites();
    }

    /** button call handlers
    **
    */
    public void NextButtonClicked() {
        DialogueManager.Instance?.FetchNextDialogue();
    }

    public void Record() {
        
    }
}
