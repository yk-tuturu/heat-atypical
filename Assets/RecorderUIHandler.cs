using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RecorderUIHandler : MonoBehaviour
{
    public CanvasGroup mainMenuPanel;
    public CanvasGroup recorderSprite;
    public GameObject confirmDeletePanel;

    public Transform scrollParent;
    public GameObject recordedLinePrefab;
    public GameObject emptyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuPanel.gameObject.SetActive(false);
        recorderSprite.alpha = 0f;
        MenuManager.Instance.openRecorderMenu += OpenRecorderMenu;
        MenuManager.Instance.closeRecorderMenu += CloseRecorderMenu;
        RecorderManager.Instance.onUpdateRecording += GetRecordings;
    }

    public void OpenRecorderMenu() {
        mainMenuPanel.gameObject.SetActive(true);
        mainMenuPanel.alpha = 0f;
        mainMenuPanel.DOFade(1f, 0.5f);

        if (!DialogueManager.Instance.currentlyInDialogue) {
            recorderSprite.DOFade(1f, 0.5f);
        }

        GetRecordings();
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

        foreach (Transform child in scrollParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (RecordableDialogue dialogue in list) {
            GameObject clone = Instantiate(recordedLinePrefab, scrollParent);
            clone.GetComponent<recordedLinePanel>().Setup(dialogue);
        }

        StartCoroutine(ForceRefreshLayout());
    }

    public void ConfirmDelete(RecordableDialogue dialogue) {
        confirmDeletePanel.SetActive(true);
        confirmDeletePanel.GetComponent<confirmDeletePanel>()?.SetDialogue(dialogue);
    }

    // dumb hack that actually works
    IEnumerator ForceRefreshLayout() {
        yield return new WaitForSeconds(0.1f);
        GameObject clone = Instantiate(emptyPrefab, scrollParent);
        yield return new WaitForSeconds(0.02f);
        GameObject.Destroy(clone);
    }
}
