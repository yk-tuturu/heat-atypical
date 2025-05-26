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
    private RectTransform storageFullPrefab;
    [SerializeField]
    private RectTransform confirmSkipPanel;

    [SerializeField]
    private DialogueSpriteManager spriteManager;

    public bool isTyping = false;
    private bool recordable = false;
    public bool disabled = false;
    public float textDelay = 0.02f;
    private Dialogue currentLine;
    private TextMeshProUGUI currentTextAsset;

    public GameObject speechTextPanel;
    public GameObject recordableSpeechPanel;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        allUIPanel.SetActive(false);
        currentTextAsset = speechText;

        RecorderManager.Instance.onRecordAdded += OnRecord;
        MenuManager.Instance.openRecorderMenu += FadeOutDialogueUI;
        MenuManager.Instance.closeRecorderMenu += FadeInDialogueUI;
        MenuManager.Instance.openDialogueMenu += StartDialogue;
        MenuManager.Instance.closeDialogueMenu += EndDialogue;
    }

    public void ShowDialoguePanel() {
        allUIPanel.SetActive(true);
    }

    public void HideDialoguePanel() {
        allUIPanel.SetActive(false);
    }

    public void StartDialogue() {
        ShowDialoguePanel();
        DialogueManager.Instance.FetchNextDialogue();
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
            speechTextPanel.SetActive(false);
            recordableSpeechPanel.SetActive(true);
        } else {
            recordable = false;
            currentTextAsset = speechText;
            speechTextPanel.SetActive(true);
            recordableSpeechPanel.SetActive(false);
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
        if (disabled) return;
        
        DialogueManager.Instance?.FetchNextDialogue();
    }

    public void SkipButtonClicked() {
        if (disabled) return;

        confirmSkipPanel.gameObject.SetActive(true);

        confirmSkipPanel.localScale = Vector3.zero;
        confirmSkipPanel.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        AudioManager.Instance.PlaySFX("uiClick");
    }

    public void confirmSkip() {
        if (disabled) return;

        confirmSkipPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(()=> {
            confirmSkipPanel.gameObject.SetActive(false);
        });
        DialogueManager.Instance?.FetchNextDialogue();

        AudioManager.Instance.PlaySFX("cassetteEject");
    }

    public void dontSkip() {
        if (disabled) return;

        confirmSkipPanel.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(()=> {
            confirmSkipPanel.gameObject.SetActive(false);
        });

        AudioManager.Instance.PlaySFX("uiClick");
    }

    public void OnRecord(RecordableDialogue dialogue) {
        if (disabled) return;

        RectTransform clone = Instantiate(notifPrefab, this.GetComponent<RectTransform>());
        clone.anchoredPosition = notifSpawnPoint.anchoredPosition;

        skipButton.SetActive(false);
        nextButton.SetActive(true);
    }

    public void FadeOutDialogueUI() {
        if (!DialogueManager.Instance.currentlyInDialogue) return;

        CanvasGroup canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 0.5f);
        canvasGroup.blocksRaycasts = false;
        disabled = true;
    }

    public void FadeInDialogueUI() {
        if (!DialogueManager.Instance.currentlyInDialogue) return;

        CanvasGroup canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(1f, 0.5f);
        canvasGroup.blocksRaycasts = true;
        disabled = false;
    }

    public void StorageFullNotif() {
        RectTransform clone = Instantiate(storageFullPrefab, this.GetComponent<RectTransform>());
        clone.anchoredPosition = notifSpawnPoint.anchoredPosition;
    }

    public void EndingTextSkipping() {
        if (!isTyping) {
            DialogueManager.Instance?.FetchNextDialogue();
        } else {
            SkipTextAnimation();
        }
    }
}
