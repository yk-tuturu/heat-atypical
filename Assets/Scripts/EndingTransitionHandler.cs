using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingTransitionHandler : MonoBehaviour
{
    public CanvasGroup blackTransition;
    public string dialogueToTriggerEnding;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.OnDialogueEnd += CheckEnding;

        blackTransition.alpha = 1f;
        blackTransition.DOFade(0f, 0.8f).OnComplete(()=> {
            StartCoroutine(WaitAndStart());
        });
    }

    void CheckEnding(string dialogueId) {
        if (dialogueId == dialogueToTriggerEnding) {
            blackTransition.DOFade(1f, 1.2f).OnComplete(()=> {
                SceneManager.LoadScene("ending");
            });
        }
    }

    IEnumerator WaitAndStart() {
        yield return new WaitForSeconds(0.1f);
        DialogueManager.Instance.TriggerDialogue("intro");
    }
}
