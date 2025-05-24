using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueUIHandler : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI speakerText;
    [SerializeField] 
    private TextMeshProUGUI speechText;
    [SerializeField]
    private TextMeshProUGUI recordableText;
    [SerializeField] 
    private GameObject allUIPanel;
    [SerializeField] 
    private GameObject dialoguePanel;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject skipButton;
    [SerializeField]
    private RectTransform notifSpawnPoint;
    [SerializeField]
    private RectTransform notifPrefab;
    [SerializeField]
    private RectTransform confirmSkipPanel;

    [SerializeField]
    private DialogueSpriteManager spriteManager;

    public bool isTyping = false;
    private bool recordable = false;
    public float textDelay = 0.02f;
    private Dialogue currentLine;
    private TextMeshProUGUI currentTextAsset;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        dialoguePanel.SetActive(false);
        currentTextAsset = speechText;
        RecorderManager.Instance.onRecordAdded += OnRecord;
    }

    public void ShowDialoguePanel() {
        allUIPanel.SetActive(true);
    }

    public void HideDialoguePanel() {
        allUIPanel.SetActive(false);
    }

    public void DisplayNextDialogue(Dialogue dialogue) {
        currentTextAsset.text = "";

        currentLine = dialogue;

        // set speaker text and sprite
        speakerText.text = dialogue.speaker;
        spriteManager.ShowSprite(dialogue.speaker);
        
        nextButton.SetActive(false);
        skipButton.SetActive(false);

        if (dialogue is RecordableDialogue) {
            recordable = true;
            currentTextAsset = recordableText;
        } else {
            recordable = false;
            currentTextAsset = speechText;
        }

        // type new sentence
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.sentence));
    }

    public void SkipTextAnimation() {
        if (!isTyping) return;

        StopAllCoroutines();
        currentTextAsset.text = currentLine.sentence;
        EndTyping();
    }


    IEnumerator TypeSentence(string sentence)
    {
        // play text audio here
        currentTextAsset.text = "";
        isTyping = true;
        foreach(char letter in sentence.ToCharArray())
        {
            currentTextAsset.text += letter;
            yield return new WaitForSeconds(textDelay);
        }
        EndTyping();
    }

    public void EndTyping() {
        isTyping = false;
        currentTextAsset.text = currentLine.sentence;

        bool isRecorded = RecorderManager.Instance.isRecorded(currentLine);

        if (recordable && !isRecorded) {
            skipButton.SetActive(true);
        } else {
            nextButton.SetActive(true);
        }
    }

    public void EndDialogue() {
        isTyping = false;
        currentTextAsset.text = "";
        speakerText.text = "";
        HideDialoguePanel();
        spriteManager.HideAllSprites();
    }

    /** 
    ** button call handlers
    */
    public void NextButtonClicked() {
        DialogueManager.Instance?.FetchNextDialogue();
    }

    public void SkipButtonClicked() {
        confirmSkipPanel.gameObject.SetActive(true);

        confirmSkipPanel.localScale = Vector3.zero;
        confirmSkipPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void confirmSkip() {
        confirmSkipPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(()=> {
            confirmSkipPanel.gameObject.SetActive(false);
        });
        DialogueManager.Instance?.FetchNextDialogue();
    }

    public void dontSkip() {
        confirmSkipPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(()=> {
            confirmSkipPanel.gameObject.SetActive(false);
        });
    }

    public void OnRecord(RecordableDialogue dialogue) {
        RectTransform clone = Instantiate(notifPrefab, this.GetComponent<RectTransform>());
        clone.anchoredPosition = notifSpawnPoint.anchoredPosition;

        skipButton.SetActive(false);
        nextButton.SetActive(true);
    }

    public void FadeOutDialogueUI() {
        CanvasGroup canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 0.7f);
    }

    public void FadeInDialogueUI() {
        CanvasGroup canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1f, 0.7f);
    }
}
