using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public CanvasGroup blackRect;

    // Start is called before the first frame update
    void Start()
    {
        blackRect.alpha = 1f;

        StartCoroutine(WaitAndStartEnding());
    }

    IEnumerator WaitAndStartEnding() {
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.SetBGMVolume(0.4f);
        AudioManager.Instance.PlayBGM("netsu_ijou");

        blackRect.DOFade(0f, 0.7f).OnComplete(()=> {
            int positiveScore = RecorderManager.Instance.CountScore(true);
            int negativeScore = RecorderManager.Instance.CountScore(false);

            if (positiveScore > negativeScore) {
                DialogueManager.Instance.TriggerDialogue("goodEnd");
            } else {
                DialogueManager.Instance.TriggerDialogue("badEnd");
            }
        });

        

        
    }
}
