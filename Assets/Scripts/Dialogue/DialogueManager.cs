using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;  
using System.IO;  
using System.Text;

public class DialogueManager : MonoBehaviourSingleton<DialogueManager>
{
    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    public bool currentlyInDialogue = false;
    private string currentDialogueID = "";
    private Dialogue currentLine;
    public DialogueUIHandler uiHandler;

    public event Action nextLine;
    public event Action<string> OnDialogueEnd;

    public bool isEndingScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (uiHandler == null) {
            Debug.Log("Dialogue UI Handler not assigned!!");
        }
        InputManager.Instance.OnMouseClicked += OnMouseClick;
    }

    void OnMouseClick(List<GameObject> objectsClicked) {
        if (isEndingScene) return;
        
        if (currentlyInDialogue) {
            if (uiHandler.isTyping) {
                uiHandler.SkipTextAnimation();
            }
        }
    }

    public Dialogue GetCurrentDialogue() {
        return currentLine;
    }

    public void FetchNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = dialogueQueue.Dequeue();
        uiHandler.DisplayNextDialogue(currentLine);

        currentlyInDialogue = true;
        nextLine?.Invoke();
    }

    public void EndDialogue() {
        OnDialogueEnd?.Invoke(currentDialogueID);

        currentlyInDialogue = false;
        currentDialogueID = "";
        MenuManager.Instance.CloseDialogueMenu();
    }

    public void TriggerDialogue(string filename)
    {
        if (currentlyInDialogue) {
            Debug.Log("currently in dialogue!!");
            return;
        }

        ReadFile(filename);

        MenuManager.Instance.OpenDialogueMenu();
        currentDialogueID = filename;
    }

    // enqueues all the dialogues from the specified file
    // remember to add a filenotfound error check later 
    public void ReadFile(string filename)
    {
        TextAsset file = (TextAsset)Resources.Load("Dialogues/" + filename);

        using (StringReader sr = new StringReader(file.text))
        {
            string line;
            int counter = 0;
            Dialogue temp = new Dialogue();

            while ((line = sr.ReadLine()) != null)
            {
                if (counter%3 == 0)
                {
                    if (line.Trim() == "+") {
                        RecordableDialogue rTemp = new RecordableDialogue();
                        rTemp.isPositive = true;
                        temp=rTemp;
                    } else if (line.Trim() == "-") {
                        RecordableDialogue rTemp = new RecordableDialogue();
                        rTemp.isPositive = false;
                        temp=rTemp;
                    }
                }
                else if (counter%3 == 1)
                {
                    temp.speaker = line;
                }
                else if (counter%3 == 2)
                {
                    temp.sentence = line;
                    temp.id = filename + "_" + Mathf.Floor(counter / 3);
                    dialogueQueue.Enqueue(temp);
                    temp = new Dialogue();
                }
                counter++;
            }
        }
    }
}
