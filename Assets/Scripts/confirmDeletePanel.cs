using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmDeletePanel : MonoBehaviour
{
    public RecordableDialogue dialogueToDelete;

    public void SetDialogue(RecordableDialogue dialogue) {
        dialogueToDelete = dialogue;
    }

    public void Delete() {
        if (dialogueToDelete == null) return;

        RecorderManager.Instance.Delete(dialogueToDelete);
        gameObject.SetActive(false);
        AudioManager.Instance?.PlaySFX("cassetteEject");
    }

    public void Close() {
        gameObject.SetActive(false);
        dialogueToDelete = null;
        AudioManager.Instance?.PlaySFX("closeCassette");
    }
}
