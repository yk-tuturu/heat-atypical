using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RecorderManager : MonoBehaviour
{
    public static RecorderManager Instance;

    public Dictionary<string, RecordableDialogue> recordedLines = new Dictionary<string, RecordableDialogue>();
    public event Action<RecordableDialogue> onRecordAdded;

    public List<RecordableDialogue> recordedList = new List<RecordableDialogue>(); // for debug

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
