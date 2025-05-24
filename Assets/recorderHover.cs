using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class recorderHover : MonoBehaviour
{
    public RectTransform recorderSprite;
    public float offset = 50f;
    public Vector2 extendPos;
    public Vector2 ogPos;
    public Vector2 menuPos;

    public DialogueUIHandler dialogueUI;
    // Start is called before the first frame update
    void Start()
    {
        ogPos = recorderSprite.anchoredPosition;
        extendPos = ogPos + new Vector2(0, offset);
        menuPos = ogPos + new Vector2(0, 150f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHoverEnter() {
        recorderSprite.DOAnchorPos(extendPos, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void OnHoverExit() {
        recorderSprite.DOAnchorPos(ogPos, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void OnPointerDown() {
        recorderSprite.DOAnchorPos(menuPos, 0.5f).SetEase(Ease.InOutQuad);
        dialogueUI.FadeOutDialogueUI();
    }
}
