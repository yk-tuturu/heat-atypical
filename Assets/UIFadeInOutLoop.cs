using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFadeInOutLoop : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    private Tween fadeTween;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
    }

    void OnEnable() {
        Debug.Log("start tween");
        fadeTween = canvasGroup.DOFade(0.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine).Play();
    }

    void OnDisable() {
        fadeTween.Kill();
    }
}
