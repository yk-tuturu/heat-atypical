using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recordableText : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color hoverColor = Color.yellow;
    public Color recordedColor = Color.yellow;
    private bool isHovering = false;
    private bool isRecorded = false;

    private Camera cam;
    public TextMeshProUGUI text;
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
            Dialogue dialogue = DialogueManager.Instance.GetCurrentDialogue();

            if (dialogue is RecordableDialogue) {
                RecordableDialogue rDialogue = (RecordableDialogue) dialogue;
                RecorderManager.Instance?.Record(rDialogue);
            } 
        }
    }

    void Reset() {
        isRecorded = false;
    }
}
