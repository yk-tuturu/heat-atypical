using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocationManager : MonoBehaviourSingleton<LocationManager>
{
    public event Action<string> changeLocation;

    public Dictionary<string, List<string>> dialoguesPerLocation = new Dictionary<string, List<string>>();
    public Dictionary<string, bool> dialogueHistory = new Dictionary<string, bool>();

    public Dictionary<string, int> storagePerLocation = new Dictionary<string, int> {
        {"lab", 3},
        {"hall", 5},
        {"store", 6},
        {"wasteland", 1}
    };

    public string currentLocation = "lab";
    // Start is called before the first frame update
    void Start()
    {
        locationUIHandler[] locationUIAll = FindObjectsOfType<locationUIHandler>();
        if (locationUIAll.Length > 0) {
            dialoguesPerLocation = locationUIAll[0].GetAllDialoguesPerLocation();
            
            foreach (KeyValuePair<string, List<string>> pair in dialoguesPerLocation) {
                List<string> dialogues = pair.Value;
                foreach (var dialogue in dialogues) {
                    dialogueHistory[dialogue] = false;
                }
            }
        }

        DialogueManager.Instance.OnDialogueEnd += OnDialogueEnd;
    }

    public void ChangeLocation(string location) {
        currentLocation = location;
        changeLocation?.Invoke(location);
        
    }

    public bool CheckLocationCompleted(string location) {
        if (!dialoguesPerLocation.ContainsKey(location)) {
            Debug.Log("couldn't find location");
            return false;
        }

        List<string> dialogues = dialoguesPerLocation[location];
        foreach (var dialogue in dialogues) {
            if (dialogueHistory[dialogue] == false) {
                return false;
            }
        }

        return true;
    }

    public void OnDialogueEnd(string dialogueId) {
        if (!dialogueHistory.ContainsKey(dialogueId)) {
            Debug.Log("couldn't find dialogue id");
            return; 
        }

        dialogueHistory[dialogueId] = true;
    }

    public int GetCurrentStorageLimit() {
        return storagePerLocation[currentLocation];
    }

    public List<string> GetDialoguesInCurrentLocation() {
        return dialoguesPerLocation[currentLocation];
    }
}
