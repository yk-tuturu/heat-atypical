using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class generalUIManager : MonoBehaviour
{
    public RectTransform rightButtons; 

    private Vector2 ogPos;
    private Vector2 hiddenPos;

    void Start() {
        MenuManager.Instance.openDialogueMenu += HideUIButtons;
        MenuManager.Instance.closeDialogueMenu += ShowUIButtons;

        ogPos = rightButtons.anchoredPosition;
        hiddenPos = ogPos + new Vector2(0f, 200f);
    }

    void HideUIButtons() {
        rightButtons.DOAnchorPos(hiddenPos, 0.5f).SetEase(Ease.InOutQuad);
    }

    void ShowUIButtons() {
        rightButtons.DOAnchorPos(ogPos, 0.5f).SetEase(Ease.InOutQuad);
    }
}
