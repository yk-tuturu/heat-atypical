using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecorderManager : MonoBehaviour
{
    public List<string> recordedLines = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Record(string line) {
        recordedLines.Add(line);
    }


}
