using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueSequenceScriptableObject", menuName = "ScriptableObjects/Dialogue/DialogueSequenceScriptableObject")]
public class DialogueSequenceScriptableObject : ScriptableObject
{
    public string ID;

    [SerializeReference]
    public List<Dialogue> dialogues;
}
