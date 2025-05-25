using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class recordableText : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color recordedColor = Color.yellow;
    private bool isHovering = false;
    private bool isRecorded = false;

    private Camera cam;
    public TextMeshProUGUI text;
    public DialogueUIHandler uiHandler;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        text.color = normalColor;
        InputManager.Instance.OnMouseClicked += OnMouseClick;
        DialogueManager.Instance.nextLine += Reset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        // Detect if any character is being hovered
        int charIndex = TMP_TextUtilities.FindIntersectingCharacter(text, mousePos, null, true);

        if (charIndex != -1 && text.textInfo.characterInfo[charIndex].isVisible)
        {
            text.color = hoverColor;
            isHovering = true;
        }
        else
        {
            text.color = normalColor;
            isHovering = false;
        }

        Dialogue currentLine = DialogueManager.Instance.GetCurrentDialogue();
        if (RecorderManager.Instance.isRecorded(currentLine)) {
            text.color = recordedColor;
            isRecorded = true;
        }
    }

    void OnMouseClick(List<GameObject> objectsClicked) {
        if (MenuManager.Instance.currentMenu != MenuManager.MenuType.Dialogue) return;

        if (isHovering && !isRecorded) {
            if (RecorderManager.Instance.checkRecorderFull()) {
                // send a notif and shake the text
                uiHandler.StorageFullNotif();

                RectTransform rect = GetComponent<RectTransform>();
                rect.DOShakeAnchorPos(
                    duration: 0.5f,     // duration of the shake
                    strength: new Vector2(10, 10), // shake range
                    vibrato: 50,        // how many times it shakes
                    randomness: 90,     // how random the shake is
                    snapping: false,
                    fadeOut: true       // fade out the shake over time
                );

                AudioManager.Instance?.PlaySFX("error");
                return;
            }

            Dialogue dialogue = DialogueManager.Instance.GetCurrentDialogue();

            if (dialogue is RecordableDialogue) {
                RecordableDialogue rDialogue = (RecordableDialogue) dialogue;
                RecorderManager.Instance?.Record(rDialogue);
                AudioManager.Instance?.PlaySFX("insertCassette");
            } 
        }
    }

    void Reset() {
        isRecorded = false;
    }
}
