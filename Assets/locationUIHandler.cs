using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class locationUIHandler : MonoBehaviour
{   
    public List<GameObject> locations = new List<GameObject>();

    private Dictionary<string, GameObject> locationsDict = new Dictionary<string, GameObject>();

    public CanvasGroup blackTransition;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject location in locations) {
            locationsDict[location.name] = location;
        }

        LocationManager.Instance.changeLocation += ChangeLocation;

        foreach (GameObject panel in locations) {
            panel.SetActive(false);
        }

        locationsDict["lab"].SetActive(true);
    }

    // can add a fade effect later
    void ChangeLocation(string location) {
        if (!locationsDict.ContainsKey(location)) {
            Debug.Log("location not found");
            return;
        }

        Sequence sequence = DOTween.Sequence();
        sequence.Append(blackTransition.DOFade(1f, 0.5f).OnComplete(()=> {
            foreach (GameObject panel in locations) {
                panel.SetActive(false);
            }

            locationsDict[location].SetActive(true);
        }));

        sequence.Append(blackTransition.DOFade(0f, 0.5f));
    }

    public Dictionary<string, List<string>> GetAllDialoguesPerLocation() {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

        foreach (GameObject location in locations) {
            Transform interactableParent = location.transform.Find("interactables");
            List<string> dialoguesList = new List<string>();

            foreach (Transform child in interactableParent) {
                dialogueInteractable dialogue = child.GetComponent<dialogueInteractable>();
                if (dialogue != null) {
                    dialoguesList.AddRange(dialogue.GetAllDialogues());
                }
            }

            result[location.name] = dialoguesList;
        }

        return result;
    }

    
}
