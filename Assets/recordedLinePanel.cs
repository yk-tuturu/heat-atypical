using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recordedLinePanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RecordableDialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setup(RecordableDialogue dialogue) {
        text.text = dialogue.sentence;
        this.dialogue = dialogue;
    }

    public void OnDelete() {
        
    }
}
