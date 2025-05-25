using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RecorderUIHandler : MonoBehaviour
{
    public CanvasGroup mainMenuPanel;
    public CanvasGroup recorderSprite;

    public Transform scrollParent;
    public GameObject recordedLinePrefab;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.gameObject.SetActive(false);
        recorderSprite.alpha = 0f;
        MenuManager.Instance.openRecorderMenu += OpenRecorderMenu;
        MenuManager.Instance.closeRecorderMenu += CloseRecorderMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenRecorderMenu() {
        mainMenuPanel.gameObject.SetActive(true);
        mainMenuPanel.alpha = 0f;
        mainMenuPanel.DOFade(1f, 0.5f);

        if (!DialogueManager.Instance.currentlyInDialogue) {
            recorderSprite.DOFade(1f, 0.5f);
        }

        foreach (Transform child in scrollParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < 5; i++) {
            Instantiate(recordedLinePrefab, scrollParent);
        }
    }

    public void CloseButtonPressed() {
        MenuManager.Instance.CloseRecorderMenu();
    }

    public void CloseRecorderMenu() {
        Debug.Log("closing panel");
        mainMenuPanel.DOFade(0f, 0.5f).OnComplete(()=>{
            mainMenuPanel.gameObject.SetActive(false);
        });
        recorderSprite.DOFade(0f, 0.5f);
    }

    public void GetRecordings() {
        List<RecordableDialogue> list = RecorderManager.Instance.GetRecordings();
    }
}
