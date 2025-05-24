using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{   
    public string speaker;
    public string sentence;
}

[System.Serializable]
public class RecordableDialogue : Dialogue
{   
    public bool isPositive;
}
