using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

// rewrite this to somehow split the dialogues by location
public class RecorderManager : MonoBehaviour
{
    public static RecorderManager Instance;

    public Dictionary<string, RecordableDialogue> recordedLines = new Dictionary<string, RecordableDialogue>();
    
    public List<RecordableDialogue> recordedList = new List<RecordableDialogue>(); // for debug

    public event Action onUpdateRecording;
    public event Action<RecordableDialogue> onRecordAdded;

    void Awake()
    {
        // If an instance already exists and it's not this:
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public void Record(RecordableDialogue dialogue) {
        recordedLines[dialogue.id] = dialogue;
        recordedList.Add(dialogue);
        onRecordAdded?.Invoke(dialogue);
    }

    public bool isRecorded(Dialogue dialogue) {
        if (!(dialogue is RecordableDialogue)) {
            return false;
        }

        return recordedLines.ContainsKey(dialogue.id);
    }

    public List<RecordableDialogue> GetRecordings() {
        return recordedLines.Values.ToList();
    }

    public void Delete(RecordableDialogue dialogue) {
        recordedLines.Remove(dialogue.id);
        onUpdateRecording?.Invoke();
    }

    public List<RecordableDialogue> GetRecordedDialogueInCurrentLocation() {
        List<string> dialoguesList = LocationManager.Instance.GetDialoguesInCurrentLocation();
        Debug.Log(dialoguesList);
        List<RecordableDialogue> result = new List<RecordableDialogue>();

        foreach (RecordableDialogue dialogue in recordedLines.Values) {
            string id = dialogue.id;
            Debug.Log(dialogue.id);
            string dialogueName = id.Split("_")[0];

            if (dialoguesList.Contains(dialogueName)) {
                result.Add(dialogue);
            }
        }

        return result;
    }

    public bool checkRecorderFull() {
        int limit = LocationManager.Instance.GetCurrentStorageLimit();
        int count = GetRecordedDialogueInCurrentLocation().Count;

        return count >= limit;
    }

    public int CountScore(bool positive) {
        int counter = 0;

        foreach (RecordableDialogue dialogue in recordedLines.Values) {
            if (dialogue.isPositive == positive) {
                counter++;
            }
        }

        return counter;
    }


}
