using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageIndicator : MonoBehaviour
{
    public GameObject emptyNotch;
    public GameObject fullNotch;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        

        LocationManager.Instance.changeLocation += Refresh;
        RecorderManager.Instance.onRecordAdded += Refresh;
        RecorderManager.Instance.onUpdateRecording += Refresh;

        StartCoroutine(WaitandRefresh());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateEmpty() {
        int limit = LocationManager.Instance.GetCurrentStorageLimit();
        for (var i = 0; i < limit; i++) {
            Instantiate(emptyNotch, parent);
        }
    }

    public void Refresh() {
        foreach (Transform child in parent) {
            GameObject.Destroy(child.gameObject);
        }

        int limit = LocationManager.Instance.GetCurrentStorageLimit();

        int count = RecorderManager.Instance.GetRecordedDialogueInCurrentLocation().Count;

        for (var i = 0; i < count; i++) {
            Instantiate(fullNotch, parent);
        }

        for (var j = 0; j < limit-count; j++) {
            Instantiate(emptyNotch, parent);
        }
        
    }

    public void Refresh(RecordableDialogue dialogue) {
        Refresh();
    }

    public void Refresh(string str) {
        Refresh();
    }

    IEnumerator WaitandRefresh() {
        yield return new WaitForSeconds(0.1f);
        Refresh();
    }
}
